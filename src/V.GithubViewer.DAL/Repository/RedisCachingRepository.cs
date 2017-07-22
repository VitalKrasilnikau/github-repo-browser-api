using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace V.GithubViewer.DAL.Repository
{
    public class RedisCachingRepository : IRedisRepository
    {
        private readonly IRedisRepository _inner;
        private readonly ConcurrentDictionary<string, uint> _cache;
        private readonly ConcurrentQueue<QueueItem> _queue;
        private readonly TimeSpan _throttlingInterval;
        private readonly CancellationToken _cancellationToken;
        private volatile int _taskIsRunning;

        public RedisCachingRepository(string connectionString, TimeSpan throttlingInterval,
            CancellationToken cancellationToken)
        {
            _inner = new RedisRepository(connectionString);
            _cache = new ConcurrentDictionary<string, uint>();
            _queue = new ConcurrentQueue<QueueItem>();
            _throttlingInterval = throttlingInterval;
            _cancellationToken = cancellationToken;
        }

        public uint Get(string key) => _cache.GetOrAdd(key, _inner.Get);

        public void Increment(string key, uint value = 1)
        {
            _queue.Enqueue(new QueueItem { key = key, Amount = value });
            RunQueueTask();
        }

        private async Task ProcessQueueAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                QueueItem queueItem;
                if (_queue.TryDequeue(out queueItem))
                {
                    _inner.Increment(queueItem.key, queueItem.Amount);
                    uint ret;
                    _cache.TryRemove(queueItem.key, out ret);
                    await Task.Delay(_throttlingInterval, cancellationToken);
                }
                else
                {
                    Interlocked.Decrement(ref _taskIsRunning);
                    if (_queue.Count > 0)
                    {
                        RunQueueTask();
                    }
                    break;
                }
            }
        }

        private void RunQueueTask()
        {
            if (Interlocked.CompareExchange(ref _taskIsRunning, 1, 0) == 0)
            {
                Task.Run(async () => await ProcessQueueAsync(_cancellationToken), _cancellationToken);
            }
        }

        struct QueueItem
        {
            internal string key;
            internal uint Amount;
        }
    }
}
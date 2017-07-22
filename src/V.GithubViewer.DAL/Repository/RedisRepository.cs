using ServiceStack.Redis;

namespace V.GithubViewer.DAL.Repository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IRedisClient _client;

        public RedisRepository(string connectionString)
        {
            _client = new RedisManagerPool(connectionString).GetClient();
        }

        public uint Get(string key) => _client.Get<uint>(key);

        public void Increment(string key, uint value = 1) =>
            _client.Increment(key, value);

        ~RedisRepository()
        {
            if (_client != null)
            {
                _client.Dispose();
            }
        }
    }
}
using ServiceStack.Redis;

namespace V.GithubViewer.DAL.Repository
{
    public class RedisRepository
    {
        private readonly string _connectionString;

        public RedisRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public uint Get(string key)
        {
            var manager = new RedisManagerPool(_connectionString);
            using (var client = manager.GetClient())
            {
                return client.Get<uint>(key);
            }
        }

        public void Increment(string key, uint value = 1)
        {
            var manager = new RedisManagerPool(_connectionString);
            using (var client = manager.GetClient())
            {
                client.Increment(key, value);
            }
        }
    }
}
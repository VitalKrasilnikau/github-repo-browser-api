namespace V.GithubViewer.DAL.Repository
{
    public interface IRedisRepository
    {
         uint Get(string key);
         void Increment(string key, uint value = 1);
    }
}
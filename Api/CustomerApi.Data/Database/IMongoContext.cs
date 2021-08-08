using MongoDB.Driver;

namespace CustomerApi.Data.Database
{
    public interface IMongoContext<out T>
    {
		IMongoCollection<T> GetCollection<T>();
    }
}

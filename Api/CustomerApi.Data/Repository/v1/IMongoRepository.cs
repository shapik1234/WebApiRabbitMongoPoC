namespace CustomerApi.Data.Repository.v1
{
    public interface IMongoRepository<TEntity> : IRepository<TEntity, string>
        where TEntity : class, new()
    {
    }
}
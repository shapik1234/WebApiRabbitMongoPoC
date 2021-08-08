using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Data
{
    public interface IRepository<TEntity, TIdent> where TEntity : class, new()
    {
        Task Create(TEntity obj, CancellationToken cancellationToken = default);
        void Update(TEntity obj, CancellationToken cancellationToken = default);
        void Delete(TIdent id, CancellationToken cancellationToken = default);
        Task<TEntity> GetById(TIdent id, CancellationToken cancellationToken = default);
        Task<List<TEntity>> Get(CancellationToken cancellationToken = default);
    }
}
using System.Threading.Tasks;

namespace Zaginiony24.Infrastructure
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<string> DeleteAsync(TEntity entity);
    }
}

using System.Collections.Generic;
using Posts.Model.Interface;
using System.Threading.Tasks;

namespace Posts.Persistence.Interface
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> GetById(int entityId);
        Task<int> Add(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
    }
}

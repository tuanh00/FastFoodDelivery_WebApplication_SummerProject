using Business_Layer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public interface IGenericRepository<TEntity> 
    {
        void SoftRemoveRange(List<TEntity> entities);
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<Pagination<TEntity>> ToPagination(int pageNumber = 0, int pageSize = 10);
        Task<int> SaveAsync();
    }
}

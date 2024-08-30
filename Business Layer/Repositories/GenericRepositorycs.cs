using Business_Layer.Commons;
using Business_Layer.DataAccess;
using Business_Layer.Services;
using Data_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Business_Layer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly FastFoodDeliveryDBContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(FastFoodDeliveryDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes) 
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
        

        public async Task AddAsync(TEntity entity)
        {            
            await _context.Set<TEntity>().AddAsync(entity);            
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void SoftRemoveRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }                

        public async Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10)
        {
            var itemCount = await _context.Set<TEntity>().CountAsync();
            var items = await _context.Set<TEntity>().Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        //public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    return await query.SingleOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        //}

        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var entityType = _context.Model.FindEntityType(typeof(TEntity));
            var primaryKey = entityType.FindPrimaryKey();
            var keyProperty = primaryKey.Properties.FirstOrDefault();

            if (keyProperty == null)
            {
                throw new InvalidOperationException("Không tìm thấy khóa chính cho thực thể.");
            }

            var keyPropertyName = keyProperty.Name;

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, keyPropertyName);
            var constant = Expression.Constant(id);
            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);

            return await query.SingleOrDefaultAsync(lambda);
        }


        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

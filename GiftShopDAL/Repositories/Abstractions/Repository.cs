using GiftShopCORE.Base;
using GiftShopDAL.DB;
using GiftShopDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopDAL.Repositories.Abstractions
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private protected readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(T Entity)
        {
            _dbSet.Remove(Entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbSet.AsQueryable(); // Returns an IQueryable for further manipulation
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<ICollection<T>> AddIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            // Add all the includes to the query
            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.ToListAsync();
        }
    }
}

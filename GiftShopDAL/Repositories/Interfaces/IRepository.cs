using GiftShopCORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopDAL.Repositories.Interfaces
{
    public interface IRepository <T> where T : BaseEntity, new()
    {
        Task<List<T>> GetAllAsync ();
        IQueryable<T> GetAllAsQueryable();
        Task<T> GetByIdAsync(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T Entity);
        Task<ICollection<T>> AddIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes);

    }
}

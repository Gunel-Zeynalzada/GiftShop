using GiftShopBUSINESS.VMs.CategoryVm;
using GiftShopBUSINESS.VMs.ProductVm;
using GiftShopCORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task Create(CreateCategoryVm createCategoryVm);
        Task Update(UpdateCategoryVm updateCategoryVm);
        Task Delete(int id);
    }
}

using GiftShopBUSINESS.Services.Interfaces;
using GiftShopBUSINESS.VMs.CategoryVm;
using GiftShopBUSINESS.VMs.ProductVm;
using GiftShopCORE.Entities;
using GiftShopDAL.Repositories.Abstractions;
using GiftShopDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.Services.Abstractions
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryService;

        public CategoryService(ICategoryRepository categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _categoryService.AddIncludes(
                _categoryService.GetAllAsQueryable(),
                c => c.Products
            );
            return categories.ToList();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryService.GetByIdAsync(id);
        }

        public async Task Create(CreateCategoryVm createCategoryVm)
        {
            
            var category = new Category
            {
                Name = createCategoryVm.Name,
               
            };

            
            await _categoryService.Create(category);
        }


        public async Task Update(UpdateCategoryVm updateCategoryVm)
        {
      
            var category = await _categoryService.GetByIdAsync(updateCategoryVm.Id);

            if (category == null)
            {
                throw new Exception("Category not found");
            }
           
            category.Name = updateCategoryVm.Name;
            await _categoryService.Update(category);
        }


        public async Task Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category != null)
            {
                await _categoryService.Delete(await _categoryService.GetByIdAsync(id));
            }

        }

    }
}

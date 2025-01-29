using GiftShopBUSINESS.Services.Interfaces;
using GiftShopBUSINESS.VMs.CategoryVm;
using GiftShopDAL.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [AutoValidateAntiforgeryToken]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext, ICategoryService categoryService)
        {
            _appDbContext = appDbContext;
            _categoryService = categoryService;
        }

        
        public async Task<IActionResult> Index()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            return View(categories);
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVm createCategoryVm)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Create(createCategoryVm);
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View(createCategoryVm);
        }

       
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryService.Delete(id);
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            UpdateCategoryVm updateCategoryVm = new UpdateCategoryVm()
            {
                Id = category.Id,
                Name = category.Name,

            };

            return View(updateCategoryVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryVm updateCategoryVm)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(updateCategoryVm);
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View(updateCategoryVm);
        }
    }
}


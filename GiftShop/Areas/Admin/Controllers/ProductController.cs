using GiftShopBUSINESS.Services.Interfaces;
using GiftShopBUSINESS.VMs.ProductVm;
using GiftShopCORE.Entities;
using GiftShopDAL.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static System.Reflection.Metadata.BlobBuilder;

namespace GiftShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [AutoValidateAntiforgeryToken]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(AppDbContext appDbContext, IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _appDbContext.Products.Include(p => p.Category).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productService.GetAllAsync();
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Fetch categories from the database
            var categories =await _appDbContext.Categories.ToListAsync();


            // Populate ViewBag with categories for dropdown
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm createProductVm)
        {
            await _productService.Create(createProductVm, _webHostEnvironment.WebRootPath);
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Product product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var categories = await _appDbContext.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            UpdateProductVm updateProductVm = new UpdateProductVm()
            {
                Id = id,
                Name=product.Name,
                Price=product.Price,
                CategoryId=product.CategoryId,
                ImageUrl=product.Image

            };
            return View(updateProductVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVm updateProductVm)
        {
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == updateProductVm.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("", "Category couldnt be found");
                ViewBag.Categories = new SelectList(await _appDbContext.Categories.ToListAsync(), "Id", "Name");
                return View();
            }
            await _productService.Update(updateProductVm, _webHostEnvironment.WebRootPath);
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
    }
}



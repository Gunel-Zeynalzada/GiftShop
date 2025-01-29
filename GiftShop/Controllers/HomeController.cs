using GiftShop.Models;
using GiftShopBUSINESS.Services.Abstractions;
using GiftShopBUSINESS.Services.Interfaces;
using GiftShopCORE.Entities;
using GiftShopDAL.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GiftShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;

        public HomeController(AppDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            // Get the latest 4 products by sorting by Id (assuming higher Id = newer product)
            var latestProducts = products.OrderByDescending(p => p.Id).Take(8).ToList();
            // Create the HomePage model
            var homePageModel = new HomePage
            {
                contactUs = new ContactUs(), // Initialize an empty ContactUs model
                products = latestProducts
            };

            return View(homePageModel);
        }

        public async Task<IActionResult> AllProducts(decimal? minPrice, decimal? maxPrice, int? categoryId)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            // Apply price filter if the values are provided
            if (minPrice.HasValue)
            {
                products = products.Where(p =>(decimal) p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => (decimal)p.Price <= maxPrice.Value);
            }

            // Apply category filter if the category is provided
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            // Get all the filtered products
            var allProducts = await products.ToListAsync();

            // Get all categories for the dropdown
            ViewData["Categories"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");

            // Store filter values in ViewData so the form retains them
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["CategoryId"] = categoryId;

            return View(allProducts);
        }


        [HttpGet]
        public async Task<IActionResult> Reviews(int productId)
        {
            var product = await _productService.GetProductWithComments(productId);

            if (product == null)
                return NotFound("Product not found.");
            @ViewData["NoReviewsMessage"] = "No reviews yet. Be the first to share your experience!";
            return View(product.Comments.ToList());
        }
        [HttpGet]
        public IActionResult AddReview(int productId)
        {
            var product = _productService.GetProductWithComments(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            ViewBag.ProductId = productId;
            ViewBag.UserName = User.Identity.Name;

            return View(new Comment());
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int productId, Comment comment)
        {
            comment.UserName = User.Identity.Name;
            comment.ProductId = productId;
            comment.DatePosted = DateTime.Now;

            await _productService.AddReview(productId, comment);
            return RedirectToAction("Reviews", "Home", new { productId = productId });
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

using GiftShopDAL.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GiftShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [AutoValidateAntiforgeryToken]
    public class DashBoardController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DashBoardController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            // Get the date 1 month ago from today
            var oneMonthAgo = DateTime.Now.AddMonths(-1);

            // Total Orders in the last 1 month
            var totalOrdersLastMonth = await _dbContext.Orders
                .Where(o => o.OrderDate >= oneMonthAgo)
                .CountAsync();

            // Total Revenue in the last 1 month
            var totalRevenueLastMonth = await _dbContext.Orders
                .Where(o => o.OrderDate >= oneMonthAgo)
                .SumAsync(o => o.TotalPrice);

            var topSellingProductLastMonth = await _dbContext.Orders
                .Where(o => o.OrderDate >= oneMonthAgo)  // Filter for orders in the last month
                .GroupBy(o => o.Product.Name)  // Group by product name
                .OrderByDescending(g => g.Sum(o => o.Quantity))  // Order by total quantity sold
                .Select(g => g.Key)  // Select the product name
                .FirstOrDefaultAsync();  // Get the top-selling product

            // Pass the data to the view
            ViewData["TotalOrdersLastMonth"] = totalOrdersLastMonth;
            ViewData["TotalRevenueLastMonth"] = totalRevenueLastMonth;
            ViewData["TopSellingProductLastMonth"] = topSellingProductLastMonth ?? "No products sold last month";
            var product=await _dbContext.Products.Include(y => y.Category).ToListAsync();
            return View(product);
        }
    }
}

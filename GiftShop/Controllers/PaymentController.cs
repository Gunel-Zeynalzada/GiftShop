using GiftShopBUSINESS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GiftShopCORE.Entities;

namespace GiftShop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public PaymentController(IPaymentService paymentService, IProductService productService, IOrderService orderService)
        {
            _paymentService = paymentService;
            _productService = productService;
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Checkout(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Create a default order to prefill data for the view
            var order = new Order
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1, // Default quantity
                TotalPrice = product.Price // Default total price
            };

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(int productId, string personName, string cardNumber, string expiry, string cvv, int quantity)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Create an order
            var order = new Order
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = quantity,
                TotalPrice = product.Price * quantity,
                CustomerName = personName,
                CustomerId = User.Identity.Name,
                OrderDate = DateTime.Now

            };

            // Process the payment
            var paymentSuccess = await _paymentService.ProcessPaymentAsync(productId, quantity);
            if (!paymentSuccess)
            {
                return View("Error"); // Display an error view if payment fails
            }

            await _orderService.CreateOrderAsync(order);

            TempData["SuccessMessage"] = "Your payment was successful!";
            return RedirectToAction("Confirmation");
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }
    }
}
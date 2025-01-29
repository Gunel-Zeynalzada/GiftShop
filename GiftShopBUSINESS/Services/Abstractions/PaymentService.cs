using GiftShopBUSINESS.Mapper.ProductMappers;
using GiftShopBUSINESS.Services.Interfaces;
using GiftShopCORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.Services.Abstractions
{
    public class PaymentService : IPaymentService
    {
        private readonly IProductService _productService;

        public PaymentService(IProductService productService)
        {
            _productService = productService;
        }

        // Simulate the payment process
        public async Task<bool> ProcessPaymentAsync(int productId, int quantity)
        {
            var product = await _productService.GetByIdAsync(productId);

            if (product == null)
            {
                return false; 
            }

            // Calculate the total price based on the quantity
            var totalPrice = product.Price * quantity;

            // Simulate payment processing here
            Console.WriteLine($"Processing payment for {quantity} unit(s) of {product.Name}, Total: ${totalPrice}");

            return true;
        }
    }
}

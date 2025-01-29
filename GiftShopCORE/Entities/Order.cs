using GiftShopCORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopCORE.Entities
{
    public class Order : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public Product Product { get; set; }
        public string CustomerId { get; set; } // User's ID
        public DateTime OrderDate { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.VMs.ProductVm
{
    public class CreateProductVm
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}

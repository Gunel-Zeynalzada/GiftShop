﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.VMs.CategoryVm
{
    public class CreateCategoryVm
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        
    }
}

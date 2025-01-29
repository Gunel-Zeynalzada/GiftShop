﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.VMs.UserVm
{
    public class LoginVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

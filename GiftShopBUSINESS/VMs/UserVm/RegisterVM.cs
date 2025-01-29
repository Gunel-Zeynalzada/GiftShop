using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.VMs.UserVm
{
    public class RegisterVM
    {
        [MaxLength(15)]
        [MinLength(3)]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }

        [MaxLength(15)]
        [MinLength(3)]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Surame can only contain letters.")]
        public string Surname { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z0-9]*$", ErrorMessage = "Username must start with a letter and can contain letters and numbers.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

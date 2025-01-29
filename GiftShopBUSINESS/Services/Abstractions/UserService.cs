using AutoMapper;
using GiftShopBUSINESS.Enums;
using GiftShopBUSINESS.Services.Interfaces;
using GiftShopBUSINESS.VMs.ProductVm;
using GiftShopBUSINESS.VMs.UserVm;
using GiftShopCORE.Entities;
using GiftShopCORE.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.Services.Abstractions
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        async Task IUserService.CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(UserRole)).Cast<UserRole>())
            {
                if (await _roleManager.FindByNameAsync(role.ToString()) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        async Task<bool> IUserService.LoginUser(LoginVM loginVm)
        {
            var user = await _userManager.FindByNameAsync(loginVm.UsernameOrEmail) ?? await _userManager.FindByEmailAsync(loginVm.UsernameOrEmail);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginVm.Password))
            {
                return false;
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }

        async Task<bool> IUserService.RegisterUser(RegisterVM registerVm)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerVm.Email);
            if (existingUser != null)
            {
                return false;
            }

            var existingUsernameUser = await _userManager.FindByNameAsync(registerVm.Username);
            if (existingUsernameUser != null)
            {
                return false;
            }

            var user = _mapper.Map<User>(registerVm);

            var result = await _userManager.CreateAsync(user, registerVm.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            await _userManager.AddToRoleAsync(user, UserRole.user.ToString());
            return true;
        }
    }
}

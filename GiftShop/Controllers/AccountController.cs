using GiftShopBUSINESS.Services.Interfaces;
using GiftShopBUSINESS.VMs.UserVm;
using GiftShopCORE.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace GiftShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        public AccountController(IUserService userService, SignInManager<User> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index","Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!await _userService.RegisterUser(registerVM))
            {
                ModelState.AddModelError("", "Please choose a different username or email, and ensure your password is at least 6 characters long, containing both letters and numbers");
                return View(registerVM);
            };
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            if (!await _userService.LoginUser(loginVm))
            {
                ModelState.AddModelError("", "Username or Password is not correct");
                return View(loginVm);
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            await _userService.CreateRoles();
            return RedirectToAction("Index", "Home");
        }
    }
}

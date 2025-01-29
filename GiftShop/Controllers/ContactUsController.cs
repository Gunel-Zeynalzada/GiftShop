using GiftShopBUSINESS.Services.Interfaces;
using GiftShopBUSINESS.VMs.ProductVm;
using GiftShopCORE.Entities;
using GiftShopDAL.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GiftShop.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactUsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactUs contactUs)
        {
            await _contactService.Create(contactUs);
            return RedirectToAction("Index", "Home");
        }
    }
}

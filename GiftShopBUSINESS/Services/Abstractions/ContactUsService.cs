using GiftShopBUSINESS.Services.Interfaces;
using GiftShopCORE.Entities;
using GiftShopDAL.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.Services.Abstractions
{
    public class ContactUsService:IContactService
    {
        private readonly AppDbContext _context;

        public ContactUsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(ContactUs contactUs)
        {
            await _context.Contacts.AddAsync(contactUs);
            await _context.SaveChangesAsync();
        }
    }
}

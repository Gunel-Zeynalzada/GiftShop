using GiftShopBUSINESS.Mapper.ProductMappers;
using GiftShopBUSINESS.Services.Abstractions;
using GiftShopBUSINESS.Services.Interfaces;
using GiftShopCORE.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.Extensions
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            //For Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IContactService, ContactUsService>();
            services.AddScoped<IOrderService, OrderService>();


            //For Mapper
            services.AddAutoMapper(typeof(ProductMP));


            return services;
        }
    }
}

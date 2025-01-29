using GiftShopDAL.Repositories.Abstractions;
using GiftShopDAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopDAL.Extensions
{
    public static class RepositoryServiceRegistration
    {

        public static IServiceCollection AddDALServices(this IServiceCollection services)
        {
            //For repos
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            return services;
        }
       
    }
}

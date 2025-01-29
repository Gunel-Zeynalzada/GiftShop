using GiftShopCORE.Entities;
using GiftShopDAL.DB;
using GiftShopDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace GiftShopDAL.Repositories.Abstractions
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

    }
}


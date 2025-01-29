using AutoMapper;
using GiftShopBUSINESS.VMs.ProductVm;
using GiftShopCORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace GiftShopBUSINESS.Mapper.ProductMappers
{
    public class ProductMP : Profile
    {
        public ProductMP()
        {
            CreateMap<Product, CreateProductVm>().ReverseMap();
            CreateMap<Product, UpdateProductVm>().ReverseMap();
        }
    }
}

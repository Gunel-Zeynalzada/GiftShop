using AutoMapper;
using GiftShopBUSINESS.VMs.ProductVm;
using GiftShopBUSINESS.VMs.UserVm;
using GiftShopCORE.Entities;
using GiftShopCORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.Mapper.UserMappers
{
    public class UserMP:Profile
    {
        public UserMP()
        {
            CreateMap<User, RegisterVM>().ReverseMap();
          
        }
    }
}

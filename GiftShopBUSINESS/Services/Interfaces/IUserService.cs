using GiftShopBUSINESS.VMs.UserVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterVM registerVm);
        Task<bool> LoginUser(LoginVM loginVm);
        Task CreateRoles();
    }
}

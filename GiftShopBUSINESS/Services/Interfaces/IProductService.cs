using GiftShopBUSINESS.VMs.ProductVm;
using GiftShopCORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace GiftShopBUSINESS.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task Create(CreateProductVm createProductVm,string path);
        Task Update(UpdateProductVm updateProductVm,string path);
        Task Delete(int id);
        Task<Product> GetProductWithComments(int productId);
        Task AddReview(int productId, Comment comment);                         
    }
}

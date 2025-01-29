using AutoMapper;
using GiftShopBUSINESS.Helper;
using GiftShopBUSINESS.Services.Interfaces;
using GiftShopBUSINESS.VMs;
using GiftShopBUSINESS.VMs.ProductVm;
using GiftShopCORE.Entities;
using GiftShopDAL.DB;
using GiftShopDAL.Repositories.Abstractions;
using GiftShopDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace GiftShopBUSINESS.Services.Abstractions
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        public ProductService(IMapper mapper, IProductRepository productRepository, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _commentRepository = commentRepository;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task Create(CreateProductVm createProductVm,string path)
        {
            var product = _mapper.Map<Product>(createProductVm);
            if (createProductVm.Image != null)
            {
                product.Image = createProductVm.Image.Upload(path, @"\Uploads\");
            }
            await _productRepository.Create(product);
        }

        public async Task Update(UpdateProductVm updateProductVm,string path)
        {
            var product= await _productRepository.GetByIdAsync(updateProductVm.Id);
            var imageUrl = product.Image;
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            _mapper.Map(updateProductVm, product);

            if (updateProductVm.Image != null)
            {

                product.Image = updateProductVm.Image.Upload(path, @"\Uploads\");
            }
            else
            {
                product.Image = imageUrl;
            }

            await _productRepository.Update(product);
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                await _productRepository.Delete(await _productRepository.GetByIdAsync(id));
            }
            
        }

        public async Task AddReview(int productId, Comment comment)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            product.Comments ??= new List<Comment>();

           
            comment.ProductId = productId;
            comment.DatePosted = DateTime.Now;
            product.Comments.Add(comment);

            await _commentRepository.Create(comment);
        }

        public async Task<Product> GetProductWithComments(int productId)
        {
            var query = _productRepository.GetAllAsQueryable();

            // Include comments for the product
            var productsWithComments = await _productRepository.AddIncludes(
                query.Where(p => p.Id == productId),
                p => p.Comments
            );

            return productsWithComments.FirstOrDefault();
        }

    }
}

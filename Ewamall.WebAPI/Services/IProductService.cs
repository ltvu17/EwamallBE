using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.WebAPI.Services
{
    public interface IProductService
    {
        public Task<Result<IEnumerable<Product>>> GetAllProduct();
        public Task<Result<IEnumerable<Product>>> GetProductId(int productId);
        public Task<Result<IEnumerable<Product>>> GetProductBySellerId(int sellerId);
        public Task<Result<Product>> CreateProduct(CreateProductCommand request);
        public Task<Result<Product>> UpdateProduct(int id, CreateProductCommand request);
        public Task<Result<Product>> DeleteProduct(int id);
    }
}

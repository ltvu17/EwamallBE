using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Application.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<Result<IEnumerable<Product>>> GetAllProduct()
        {
            Result<IEnumerable<Product>> result = (await _productRepo.GetAllAsync()).ToList();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<Product>>(new Error("IEnumerable<Product>.GetAll()", "Fail to load product"));
            }
            return result;
        }
    }
}

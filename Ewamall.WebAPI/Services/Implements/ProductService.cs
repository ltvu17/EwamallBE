using AutoMapper;
using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.WebAPI.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IProductRepo productRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<Result<Product>> CreateProduct(CreateProductCommand request)
        {
            var product = Product.Create(
                request.ProductName,
                request.ProductDescription,
                request.CoverImageId,
                request.ImagesId,
                request.VideoId,
                request.IndustryId,
                request.SellerId
                );
            if(product is null)
            {
                return Result.Failure<Product>(new Error("IEnumerable<Product>.GetAll()", "Fail to load product"));
            }
            var productDetail = request.ProductSellDetails;
            if (productDetail is not null) 
            {
                foreach(var detail in  productDetail)
                {
                    product.AddProductDetail(detail.DetailId, detail.Description);
                }
            }
            await _productRepo.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return product;
        }

        public async Task<Result<Product>> UpdateProduct(int id, CreateProductCommand request)
        {
            var product = await _productRepo.GetByIdAsync(id);
            var updatedProduct = _mapper.Map(request, product);
            if (product is null)
            {
                return Result.Failure<Product>(new Error("UpdateProduct.GetById", "Product not found"));
            }
            var productDetailUpdated = updatedProduct.ProductSellDetails;
            if(productDetailUpdated is not null && productDetailUpdated.GroupBy(x => x.DetailId).Any(g => g.Count() > 1))
            {
                return Result.Failure<Product>(new Error("UpdateProduct.GetById", "Duplicate details"));
            }
            /*if (productDetailUpdated is not null)
            {
                foreach (var detail in productDetailUpdated)
                {
                    if(product.ProductSellDetails.Any(s=>s.DetailId == detail.DetailId))
                    {
                        var description = product.ProductSellDetails.FirstOrDefault(s=>s.DetailId == detail.DetailId).Description;
                        if(description.Equals(detail.Description, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                    }
                }
            }*/
            await _productRepo.UpdateAsync(updatedProduct);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map(request,product);
        }

        public async Task<Result<Product>> DeleteProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product is null)
            {
                return Result.Failure<Product>(new Error("UpdateProduct.GetById", "Product not found"));
            }
            await _productRepo.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return product;
        }
    }
}

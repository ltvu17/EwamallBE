using AutoMapper;
using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;
using Ewamall.WebAPI.Helper;
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
        private readonly IIndustryAggrateRepo _industryRepo;
        private readonly ISellerRepo _sellerRepo;

        public ProductService(IProductRepo productRepo, IUnitOfWork unitOfWork, IMapper mapper, IIndustryAggrateRepo industryRepo, ISellerRepo sellerRepo)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _industryRepo = industryRepo;
            _sellerRepo = sellerRepo;
        }

        public async Task<Result<IEnumerable<ProductDTO>>> GetAllProduct()
        {
            Result<IEnumerable<ProductDTO>> result = (await _productRepo.GetAllDTOAsync()).ToList();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<ProductDTO>>(new Error("IEnumerable<ProductDTO>.GetAll()", "Fail to load product"));
            }
            return result;
        }

        public async Task<Result<Product>> CreateProduct(CreateProductCommand request)
        {
            var result = Product.Create(
                request.ProductName,
                request.ProductDescription,
                request.CoverImageId,
                request.ImagesId,
                request.VideoId,
                request.IndustryId,
                request.SellerId
                );
            if (result.IsFailure)
            {
                return Result.Failure<Product>(new Error("IEnumerable<Product>.GetAll()", "Fail to load product"));
            }
            var product = result.Value;
            var productDetail = request.ProductSellDetails;
            if (productDetail is not null) 
            {
                foreach(var detail in  productDetail)
                {
                    product.AddProductDetail(detail.DetailId, detail.Description);
                }
            }
            var productSellDetail = request.ProductSellCommand;
            if (productSellDetail is not null)
            {
                foreach (var item in productSellDetail)
                {
                    if (item.ParentNodeId == 0)
                    {
                        item.ParentNodeId = null;
                    }
                }
                product.AddProductSellDetail(_mapper.Map<IEnumerable<ProductSellDetail>>(productSellDetail));
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

        public async Task<Result<IEnumerable<ProductDTO>>> GetProductBySellerId(int sellerId)
        {
            var seller = (await _sellerRepo.GetByIdAsync(sellerId));
            if (seller is null)
            {
                return Result.Failure<IEnumerable<ProductDTO>>(new Error("GetProductBySellerId.GetByIdAsync()", "Seller not found"));
            }
            var product = (await _productRepo.GetAllDTOBySellerIdAsync(sellerId)).ToList();
            if(product.Count == 0)
            {
                    return Result.Failure<IEnumerable<ProductDTO>>(new Error("GetProductBySellerId.FindAsync()", "Product not found"));      
            }
            return product;
        }

        public async Task<Result<Product>> GetProductId(int productId)
        {
            var product = (await _productRepo.GetByIdAsync(productId));
            if (product == null)
            {
                return Result.Failure<Product>(new Error("GetProductBySellerId.FindAsync()", "Product not found"));
            }
            return product;
        }

        public async Task<Result<IEnumerable<ProductDTO>>> GetAllProductByIndustryId(int industryId)
        {
            var industry = await _industryRepo.GetByIdAsync(industryId);
            if(industry == null)
            {
                return Result.Failure<IEnumerable<ProductDTO>>(new Error("GetAllProductByIndustryId.GetByIdAsync()", "Industry not found"));
            }
            var leafIndustry = (await _industryRepo.FindAsync(s => s.IsLeaf == true && s.ParentNodeId == industry.ParentNodeId && s.Path.StartsWith(industry.Path), int.MaxValue, 1)).ToList();
            List<ProductDTO> products = new List<ProductDTO>();
            foreach (var item in leafIndustry)
            {
                var productDTO = await _productRepo.GetAllDTOByIndustryIdAsync(item.Id);
                products.AddRange(productDTO);
            }
            if(products.Count == 0)
            {
                return Result.Failure<IEnumerable<ProductDTO>>(new Error("GetAllProductByIndustryId.GetAllDTOByIndustryIdAsync()", "Product not found"));
            }
            return products;
        }

        public async Task<Result<IEnumerable<ProductDTO>>> GetAllProductBySearch(SearchCommand search)
        {
            var products = new List<ProductDTO>();
            List<ProductDTO> allProduct = (List<ProductDTO>)await _productRepo.GetAllDTOAsync();
            var search1 = allProduct.Where(s => Utils.ConvertToUnSign(s.ProductName).Contains(Utils.ConvertToUnSign(search.SearchValue), StringComparison.CurrentCultureIgnoreCase)).ToList();
            products.AddRange(search1);
            allProduct.RemoveAll(s=>search1.Contains(s));
            List<ProductDTO> search2 = new List<ProductDTO>();
            foreach(var key in search.SearchValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()))
            {
                /*                search2.AddRange(allProduct.Where(s => Utils.ConvertToUnSign(s.ProductName).Contains(Utils.ConvertToUnSign(key), StringComparison.CurrentCultureIgnoreCase)).ToList());*/
                search2.AddRange(allProduct.Where(s =>
                {
                    var searchSource = Utils.ConvertToUnSign(s.ProductName).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim().ToLower());
                    return searchSource.Contains(Utils.ConvertToUnSign(key).ToLower());
                }).ToList());
                allProduct.RemoveAll(s => search2.Contains(s));
            }
            products.AddRange(search2);
            if(products.Count == 0)
            {
                return Result.Failure<IEnumerable<ProductDTO>>(new Error("GetAllProductBySearch.FindProduct()", "Product not found"));
            }
            return products;
        }

        public async Task<Result<Product>> UpdateProductStatus(int id, int status)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product is null)
            {
                return Result.Failure<Product>(new Error("UpdateProduct.GetById", "Product not found"));
            }
            var result = product.ChangeStatusProduct(status);
            if (result.IsFailure)
            {
                return Result.Failure<Product>(new Error("UpdateProduct.ChangeStatusProduct", "Product not found"));
            }
            await _productRepo.UpdateAsync(result.Value);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}

using AutoMapper;
using Ewamall.Domain.Entities;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AuthenticationResponse, Account>();
            CreateMap<Account, AuthenticationResponse>();
            CreateMap<Role, RoleDTO>();

            CreateMap<CreateProductCommand, Product>();
            CreateMap<ProductDetailCommand, ProductDetail>();
            CreateMap<ProductSellCommand, ProductSellDetail>();
        }
    }
}

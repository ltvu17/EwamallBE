using AutoMapper;
using Ewamall.Business.Entities;
using Ewamall.Business.IRepository;
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
            CreateMap<CreateVoucherCommand, Voucher>();
            CreateMap<UpdateUserAccount, Account>();
            CreateMap<CreateSeller, Seller>();
            CreateMap<User, UserDTO>();
            CreateMap<CreateNotification, Notification>();
            CreateMap<UserInformation, User>();


            CreateMap<CreateProductCommand, Product>();
            CreateMap<ProductDetailCommand, ProductDetail>();
            CreateMap<ProductSellCommand, ProductSellDetail>();
            CreateMap<CreateIndustryAndDetailCommand, Industry>();
            CreateMap<DetailCommand, IndustryDetail>();
            CreateMap<CartDTO, CartResponse>();
            CreateMap<CreateShipAddressCommand, ShipAddress>();
        }
    }
}

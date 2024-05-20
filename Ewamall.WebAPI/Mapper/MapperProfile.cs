﻿using AutoMapper;
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

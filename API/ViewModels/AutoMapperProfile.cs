using System;
using API.DAL.Models;
using AutoMapper;

namespace API.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(d => d.CustomerId, map => map.Condition(src => src.CustomerId != Guid.Empty))
                .ReverseMap();

            CreateMap<CustomerViewModel, Customer>()
                .ForMember(d => d.CustomerId, map => map.Condition(src => src.CustomerId != Guid.Empty))
                .ReverseMap();

            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.ProductId, map => map.Condition(src => src.ProductId != Guid.Empty))
                .ReverseMap();

            CreateMap<ProductViewModel, Product>()
                .ForMember(d => d.ProductId, map => map.Condition(src => src.ProductId != Guid.Empty))
                .ReverseMap();
        }
    }
}

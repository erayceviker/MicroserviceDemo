using AutoMapper;
using Microservice.Demo.Basket.Api.Data;
using Microservice.Demo.Basket.Api.Dtos;

namespace Microservice.Demo.Basket.Api.Features.Baskets
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketDto, Data.Basket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}

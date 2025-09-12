using AutoMapper;
using MicroserviceDemo.Order.Application.Features.Orders.Create;
using MicroserviceDemo.Order.Domain.Entities;

namespace MicroserviceDemo.Order.Application.Features.Orders
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}

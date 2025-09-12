using MicroserviceDemo.Order.Application.Features.Orders.Create;

namespace MicroserviceDemo.Order.Application.Features.Orders.GetOrders
{
    public record GetOrdersResponse(DateTime Created, decimal TotalPrice, List<OrderItemDto> Items);
}

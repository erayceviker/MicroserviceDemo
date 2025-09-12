using MicroserviceDemo.Shared;

namespace MicroserviceDemo.Order.Application.Features.Orders.GetOrders
{
    public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;
}

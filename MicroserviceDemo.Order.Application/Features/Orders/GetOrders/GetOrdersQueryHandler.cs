using AutoMapper;
using MediatR;
using MicroserviceDemo.Order.Application.Contracts.Repositories;
using MicroserviceDemo.Order.Application.Features.Orders.Create;
using MicroserviceDemo.Shared;
using MicroserviceDemo.Shared.Services;

namespace MicroserviceDemo.Order.Application.Features.Orders.GetOrders
{
    public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper)
        : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByBuyerId(identityService.UserId);


            var response = orders.Select(o =>
                new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();


            return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
        }
    }
}

using MicroserviceDemo.Shared;

namespace Microservice.Demo.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;
}

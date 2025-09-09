using Microservice.Demo.Basket.Api.Dtos;
using MicroserviceDemo.Shared;

namespace Microservice.Demo.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery : IRequestByServiceResult<BasketDto>;
}

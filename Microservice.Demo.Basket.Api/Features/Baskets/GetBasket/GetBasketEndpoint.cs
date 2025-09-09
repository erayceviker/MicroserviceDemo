using MediatR;
using MicroserviceDemo.Shared.Extensions;

namespace Microservice.Demo.Basket.Api.Features.Baskets.GetBasket
{
    public static class GetBasketEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/user",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetBasketQuery())).ToGenericResult())
                .WithName("GetBasket")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}

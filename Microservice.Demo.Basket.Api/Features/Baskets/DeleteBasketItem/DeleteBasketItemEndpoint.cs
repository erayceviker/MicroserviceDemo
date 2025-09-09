using MediatR;
using MicroserviceDemo.Shared.Extensions;

namespace Microservice.Demo.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public static class GetBasketEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItem(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{id:guid}",
                    async (Guid id, IMediator mediator) =>
                        (await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResult())
                .WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status204NoContent);

            return group;
        }
    }
}

using MediatR;
using MicroserviceDemo.Shared.Extensions;
using MicroserviceDemo.Shared.Filters;

namespace Microservice.Demo.Basket.Api.Features.Baskets.AddBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/item",
                    async (AddBasketItemCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("AddBasketItem")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();

            return group;
        }
    }
}

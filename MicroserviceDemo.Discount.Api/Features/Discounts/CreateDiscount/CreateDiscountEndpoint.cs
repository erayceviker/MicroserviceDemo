using MicroserviceDemo.Shared.Filters;

namespace MicroserviceDemo.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class CreateDiscountEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                async (CreateDiscountCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateDiscount")
                .MapToApiVersion(1,0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();

            return group;
        }
    }
}

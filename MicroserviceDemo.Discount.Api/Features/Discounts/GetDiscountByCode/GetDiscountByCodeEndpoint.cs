using MicroserviceDemo.Shared.Filters;

namespace MicroserviceDemo.Discount.Api.Features.Discounts.GetDiscountByCode
{
    public static class GetDiscountByCodeEndpoint
    {
        public static RouteGroupBuilder GetDiscountByCodeGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/{code}",
                    async (string code, IMediator mediator) =>
                        (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult())
                .WithName("GetDiscountByCode")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status200OK);

            return group;
        }
    }
}

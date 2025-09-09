using MediatR;
using MicroserviceDemo.Shared.Extensions;
using MicroserviceDemo.Shared.Filters;

namespace Microservice.Demo.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupItem(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-coupon",
                    async (ApplyDiscountCouponCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("ApplyDiscountCoupon")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status200OK)
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();

            return group;
        }
    }
}

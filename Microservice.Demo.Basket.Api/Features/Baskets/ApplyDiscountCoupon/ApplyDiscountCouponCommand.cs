using MicroserviceDemo.Shared;

namespace Microservice.Demo.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public record ApplyDiscountCouponCommand(string Coupon,float DiscountRate) : IRequestByServiceResult;
}

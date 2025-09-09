using MediatR;
using MicroserviceDemo.Shared;
using System.Net;
using System.Text.Json;

namespace Microservice.Demo.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("Basket not found.",HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;

            if (!basket.Items.Any())
            {
                return ServiceResult.Error("Basket item not found",HttpStatusCode.NotFound);
            }

            basket.ApplyNewDiscount(request.Coupon,request.DiscountRate);

            await basketService.CreateBasketCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
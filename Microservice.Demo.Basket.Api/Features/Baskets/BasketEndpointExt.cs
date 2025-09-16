using Asp.Versioning.Builder;
using Microservice.Demo.Basket.Api.Features.Baskets.AddBasketItem;
using Microservice.Demo.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using Microservice.Demo.Basket.Api.Features.Baskets.DeleteBasketItem;
using Microservice.Demo.Basket.Api.Features.Baskets.GetBasket;
using Microservice.Demo.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace Microservice.Demo.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .DeleteBasketItemGroupItem()
                .GetBasketGroupItem()
                .ApplyDiscountCouponGroupItem()
                .RemoveDiscountCouponGroupItemEndpoint()
                .AddBasketItemGroupItem().RequireAuthorization("Password");
        }
    }
}

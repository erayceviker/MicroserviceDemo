using Asp.Versioning.Builder;
using MicroserviceDemo.Discount.Api.Features.Discounts.CreateDiscount;
using MicroserviceDemo.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace MicroserviceDemo.Discount.Api.Features.Discounts
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("Discounts")
                .WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItem()
                .GetDiscountByCodeGroupItem();

        }
    }
}

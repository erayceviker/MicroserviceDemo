namespace MicroserviceDemo.Discount.Api.Features.Discounts.GetDiscountByCode
{
    public record GetDiscountByCodeQuery(string Code) : IRequestByServiceResult<GetDiscountByCodeResponse>;
}
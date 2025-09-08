using MicroserviceDemo.Discount.Api.Repositories;

namespace MicroserviceDemo.Discount.Api.Features.Discounts.GetDiscountByCode
{
    public class GetDiscountByCodeQueryHandler(AppDbContext context) : IRequestHandler<GetDiscountByCodeQuery,ServiceResult<GetDiscountByCodeResponse>>
    {
        public async Task<ServiceResult<GetDiscountByCodeResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {
            var hasDiscount = await context.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken);

            if (hasDiscount is null)
            {
                return ServiceResult<GetDiscountByCodeResponse>.Error("Discount not found.", HttpStatusCode.BadRequest);
            }

            if (hasDiscount.Expired < DateTime.Now)
            {
                return ServiceResult<GetDiscountByCodeResponse>.Error("Discount is expired.",HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetDiscountByCodeResponse>.SuccessAsOk(new GetDiscountByCodeResponse(hasDiscount.Rate,hasDiscount.Code));

        }
    }
}
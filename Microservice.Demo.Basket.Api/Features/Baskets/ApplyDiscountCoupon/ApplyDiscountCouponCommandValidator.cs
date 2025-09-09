using FluentValidation;

namespace Microservice.Demo.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandValidator : AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(x => x.Coupon)
                .NotEmpty().WithMessage("Coupon is required.")
                .MaximumLength(50).WithMessage("Coupon must not exceed 50 characters.");
            RuleFor(x => x.DiscountRate)
                .GreaterThan(0).WithMessage("Discount rate must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Discount rate must be less than or equal to 100.");
        }
    }
}

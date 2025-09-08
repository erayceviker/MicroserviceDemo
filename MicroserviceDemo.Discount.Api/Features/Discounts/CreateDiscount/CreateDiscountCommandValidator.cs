namespace MicroserviceDemo.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");
            RuleFor(x => x.Rate).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Expired).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}

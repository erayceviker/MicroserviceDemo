using FluentValidation;

namespace Microservice.Demo.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.CourseName).NotEmpty().MaximumLength(100).WithMessage("{PropertyName} is required and must be 100 characters or less.");
            RuleFor(x => x.CoursePrice).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
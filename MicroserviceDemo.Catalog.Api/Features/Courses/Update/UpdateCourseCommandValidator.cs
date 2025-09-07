namespace MicroserviceDemo.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description must not exceed 500 characters");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");
            RuleFor(x => x.ImageUrl)
                .MaximumLength(200).WithMessage("ImageUrl must not exceed 200 characters")
                .When(x => !string.IsNullOrEmpty(x.ImageUrl));
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required");
        }
    }
}

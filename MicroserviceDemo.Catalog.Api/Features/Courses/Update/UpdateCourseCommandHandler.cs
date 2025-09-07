namespace MicroserviceDemo.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context,IMapper mapper) : IRequestHandler<UpdateCourseCommand,ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FindAsync(request.Id, cancellationToken);
            if (hasCourse is null)
            {
                return ServiceResult.Error("Course not found", $"The course with Id: {request.Id} was not found", HttpStatusCode.NotFound);
            }
            var hasCategory = await context.Categories.FindAsync(request.CategoryId, cancellationToken);
            if (hasCategory is null)
            {
                return ServiceResult.Error("Category not found", $"The category with Id: {request.CategoryId} was not found", HttpStatusCode.NotFound);
            }

            mapper.Map(request, hasCourse);

            context.Courses.Update(hasCourse);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}

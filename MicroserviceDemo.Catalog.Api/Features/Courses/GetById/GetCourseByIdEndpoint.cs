using MicroserviceDemo.Catalog.Api.Features.Courses.Dtos;

namespace MicroserviceDemo.Catalog.Api.Features.Courses.GetById
{

    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto?>;

    public class GetCourseByIdHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto?>>
    {
        public async Task<ServiceResult<CourseDto?>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FindAsync(request.Id, cancellationToken);
            if (hasCourse is null)
            {
                return ServiceResult<CourseDto?>.Error("Course not found", $"The course with Id: {request.Id} was not found", HttpStatusCode.NotFound);
            }

            hasCourse.Category = (await context.Categories.FindAsync(hasCourse.CategoryId, cancellationToken))!;

            var courseDto = mapper.Map<CourseDto>(hasCourse);
            return ServiceResult<CourseDto?>.SuccessAsOk(courseDto);
        }
    }

    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetCourseByIdGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) => (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult()).WithName("GetCourseById");
            return group;
        }
    }
}

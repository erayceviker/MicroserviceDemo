using MicroserviceDemo.Catalog.Api.Features.Courses.Dtos;

namespace MicroserviceDemo.Catalog.Api.Features.Courses.GetAllByUserId
{

    public record GetAllByUserIdCoursesQuery(Guid UserId) : IRequestByServiceResult<List<CourseDto>>;

    public class GetAllByUserIdCoursesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllByUserIdCoursesQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllByUserIdCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.AsNoTracking()
                .Where(c => c.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            if (!courses.Any())
            {
                return ServiceResult<List<CourseDto>>.Error("Courses not found", $"No courses found for UserId: {request.UserId}", HttpStatusCode.NotFound);
            }

            var categories = await context.Categories.AsNoTracking().ToListAsync(cancellationToken);

            foreach (var course in courses)
            {
                course.Category = categories.First(c => c.Id == course.CategoryId);
            }

            var courseDtos = mapper.Map<List<CourseDto>>(courses);

            return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtos);
        }
    }


    public static class GetAllByUserIdCoursesEndpoint
    {
        public static RouteGroupBuilder GetAllByUserIdCoursesGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}", async (Guid userId, IMediator mediator) => (await mediator.Send(new GetAllByUserIdCoursesQuery(userId))).ToGenericResult())
                .WithName("GetAllByUserId")
                .Produces<List<CourseDto>>()
                .Produces(StatusCodes.Status404NotFound);
            return group;
        }
    }
}

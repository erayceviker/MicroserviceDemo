using MicroserviceDemo.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceDemo.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseCommandEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                async ([FromForm] CreateCourseCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateCourse")
                .MapToApiVersion(1,0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>().DisableAntiforgery();

            return group;
        }
    }
}

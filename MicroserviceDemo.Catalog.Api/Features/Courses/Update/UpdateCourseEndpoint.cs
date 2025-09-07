using MicroserviceDemo.Shared.Filters;

namespace MicroserviceDemo.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseEndpoint
    {
        public static RouteGroupBuilder UpdateCourseEndpointGroupItem(this RouteGroupBuilder group)
        {
            group.MapPut("/",
                    async (UpdateCourseCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("UpdateCourse")
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();

            return group;
        }
    }
}

using MediatR;
using MicroserviceDemo.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceDemo.File.Api.Features.File.Delete
{
    public static class DeleteFileCommandEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItem(this RouteGroupBuilder group)
        {
            group.MapDelete("/",
                    async ([FromBody] DeleteFileCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("Delete")
                .MapToApiVersion(1, 0).DisableAntiforgery();

            return group;
        }
    }
}

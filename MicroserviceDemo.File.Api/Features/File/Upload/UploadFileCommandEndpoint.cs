using MediatR;
using MicroserviceDemo.Shared.Extensions;

namespace MicroserviceDemo.File.Api.Features.File.Upload
{
    public static class DeleteFileCommandEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (IFormFile file, IMediator mediator) =>
                        (await mediator.Send(new UploadFileCommand(file))).ToGenericResult())
                .WithName("Upload")
                .MapToApiVersion(1, 0).DisableAntiforgery();

            return group;
        }
    }
}

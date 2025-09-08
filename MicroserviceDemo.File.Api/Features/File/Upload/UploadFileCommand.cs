using MicroserviceDemo.Shared;

namespace MicroserviceDemo.File.Api.Features.File.Upload
{
    public record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileCommandResponse>;
}

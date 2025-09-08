using MicroserviceDemo.Shared;

namespace MicroserviceDemo.File.Api.Features.File.Delete
{
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
}

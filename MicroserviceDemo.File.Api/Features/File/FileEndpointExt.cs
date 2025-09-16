using Asp.Versioning.Builder;
using MicroserviceDemo.File.Api.Features.File.Delete;
using MicroserviceDemo.File.Api.Features.File.Upload;

namespace MicroserviceDemo.File.Api.Features.File
{
    public static class FileEndpointExt
    {
        public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files").WithTags("Files")
                .WithApiVersionSet(apiVersionSet)
                .UploadFileGroupItem()
                .DeleteFileGroupItem().RequireAuthorization();
        }
    }
}

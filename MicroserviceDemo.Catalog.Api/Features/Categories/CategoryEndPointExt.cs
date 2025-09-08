using Asp.Versioning.Builder;
using MicroserviceDemo.Catalog.Api.Features.Categories.Create;
using MicroserviceDemo.Catalog.Api.Features.Categories.GetAll;
using MicroserviceDemo.Catalog.Api.Features.Categories.GetById;

namespace MicroserviceDemo.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories")
                .WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItem()
                .GetAllCategoriesGroupItem()
                .GetByIdCategoryGroupItem();
        }
    }
}

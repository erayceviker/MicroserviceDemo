using MicroserviceDemo.Catalog.Api.Features.Categories.Create;
using MicroserviceDemo.Catalog.Api.Features.Categories.GetAll;
using MicroserviceDemo.Catalog.Api.Features.Categories.GetById;

namespace MicroserviceDemo.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").WithTags("Categories")
                .CreateCategoryGroupItem()
                .GetAllCategoriesGroupItem()
                .GetByIdCategoryGroupItem();
        }
    }
}

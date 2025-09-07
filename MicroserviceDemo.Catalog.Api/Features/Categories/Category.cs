using MicroserviceDemo.Catalog.Api.Features.Courses;
using MicroserviceDemo.Catalog.Api.Repositories;

namespace MicroserviceDemo.Catalog.Api.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;

        public List<Course>? Courses { get; set; } 
    }
}

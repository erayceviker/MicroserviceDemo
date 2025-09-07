using MicroserviceDemo.Catalog.Api.Features.Categories;
using MicroserviceDemo.Catalog.Api.Repositories;

namespace MicroserviceDemo.Catalog.Api.Features.Courses
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Picture { get; set; } 
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Created { get; set; }
        public Feature Feature { get; set; } = null!;



        // navigation property
        public Category Category { get; set; }
    }
}

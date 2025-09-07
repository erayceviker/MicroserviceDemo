using MicroserviceDemo.Catalog.Api.Features.Categories;

namespace MicroserviceDemo.Catalog.Api.Features.Courses
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; } 
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Created { get; set; }
        public Feature Feature { get; set; } = null!;



        // navigation property
        public Category Category { get; set; }
    }
}

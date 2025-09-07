using MicroserviceDemo.Catalog.Api.Features.Categories;
using MicroserviceDemo.Catalog.Api.Features.Courses;
using MongoDB.Driver;
using System.Reflection;

namespace MicroserviceDemo.Catalog.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }


        public static AppDbContext Create(IMongoDatabase database)

        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                .Options;

            return new AppDbContext(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

using System.Reflection;
using MongoDB.Driver;

namespace MicroserviceDemo.Discount.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {


        public DbSet<Features.Discounts.Discount> Discounts { get; set; } = null!;

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

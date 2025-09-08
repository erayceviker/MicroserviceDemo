using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceDemo.Discount.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }





    }
}

namespace Microservice.Demo.Basket.Api.Data
{
    public class BasketItem(Guid id, string name, string? imageUrl, decimal price, decimal? priceByApplyDiscountRate)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string? ImageUrl { get; set; } = imageUrl;
        public decimal Price { get; set; } = price;
        public decimal? PriceByApplyDiscountRate { get; set; } = priceByApplyDiscountRate;
    }
}

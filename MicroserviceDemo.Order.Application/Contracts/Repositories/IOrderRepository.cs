using MicroserviceDemo.Order.Domain.Entities;

namespace MicroserviceDemo.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<Guid, Domain.Entities.Order>
    {
        Task<List<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId);

        Task SetStatus(string orderCode, Guid paymentId, OrderStatus status);
    }
}

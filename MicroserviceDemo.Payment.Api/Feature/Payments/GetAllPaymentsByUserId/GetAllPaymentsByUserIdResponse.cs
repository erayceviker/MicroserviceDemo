using MicroserviceDemo.Payment.Api.Repositories;

namespace MicroserviceDemo.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdResponse(
        Guid Id,
        string OrderCode,
        string Amount,
        DateTime Created,
        PaymentStatus Status);
}

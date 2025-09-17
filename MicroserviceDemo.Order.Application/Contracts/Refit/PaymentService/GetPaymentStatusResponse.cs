namespace MicroserviceDemo.Order.Application.Contracts.Refit.PaymentService
{
    public record GetPaymentStatusResponse(Guid? PaymentId, bool IsPaid);

}

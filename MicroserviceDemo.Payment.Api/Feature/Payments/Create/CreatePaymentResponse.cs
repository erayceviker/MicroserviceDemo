namespace MicroserviceDemo.Payment.Api.Feature.Payments.Create
{
    public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);
}

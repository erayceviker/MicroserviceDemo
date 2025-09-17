using Asp.Versioning.Builder;
using MicroserviceDemo.Payment.Api.Feature.Payments.Create;
using MicroserviceDemo.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;
using MicroserviceDemo.Payment.Api.Feature.Payments.GetStatus;

namespace MicroserviceDemo.Payment.Api.Feature.Payments
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("payments").WithApiVersionSet(apiVersionSet)
            .CreatePaymentGroupItemEndpoint().GetAllPaymentsByUserIdGroupItemEndpoint().GetPaymentStatusGroupItemEndpoint();
        }
    }
}

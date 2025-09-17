using MicroserviceDemo.Shared;

namespace MicroserviceDemo.Payment.Api.Feature.Payments.GetStatus
{
    public record GetPaymentStatusRequest(string orderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;

}

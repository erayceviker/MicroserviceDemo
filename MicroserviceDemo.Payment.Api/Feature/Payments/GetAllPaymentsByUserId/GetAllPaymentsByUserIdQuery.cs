using MicroserviceDemo.Shared;

namespace MicroserviceDemo.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;
}

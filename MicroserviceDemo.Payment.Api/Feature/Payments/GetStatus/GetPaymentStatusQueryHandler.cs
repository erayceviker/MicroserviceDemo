using MediatR;
using MicroserviceDemo.Payment.Api.Repositories;
using MicroserviceDemo.Shared;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceDemo.Payment.Api.Feature.Payments.GetStatus
{
    public class GetPaymentStatusQueryHandler(AppDbContext context)
       : IRequestHandler<GetPaymentStatusRequest, ServiceResult<GetPaymentStatusResponse>>
    {
        public async Task<ServiceResult<GetPaymentStatusResponse>> Handle(GetPaymentStatusRequest request,
            CancellationToken cancellationToken)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(x => x.OrderCode == request.orderCode,
                cancellationToken: cancellationToken);

            if (payment is null)
            {
                return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(null, false));
            }

            return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(
                new GetPaymentStatusResponse(payment.Id, payment.Status == PaymentStatus.Success));
        }
    }
}

using MediatR;
using MicroserviceDemo.Payment.Api.Repositories;
using MicroserviceDemo.Shared;
using MicroserviceDemo.Shared.Services;

namespace MicroserviceDemo.Payment.Api.Feature.Payments.Create
{
    public class CreatePaymentCommandHandler(
        AppDbContext appDbContext,
        IIdentityService identityService,
        IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<CreatePaymentCommand, ServiceResult<CreatePaymentResponse>>
    {
        public async Task<ServiceResult<CreatePaymentResponse>> Handle(CreatePaymentCommand request,
            CancellationToken cancellationToken)
        {
            var userId = identityService.UserId;

            var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(request.CardNumber,
                request.CardHolderName,
                request.CardExpirationDate, request.CardSecurityNumber, request.Amount);


            if (!isSuccess)
            {
                return ServiceResult<CreatePaymentResponse>.Error("Payment Failed", errorMessage!,
                    System.Net.HttpStatusCode.BadRequest);
            }


            var newPayment = new Repositories.Payment(userId, request.OrderCode, request.Amount);
            newPayment.SetStatus(PaymentStatus.Success);

            appDbContext.Payments.Add(newPayment);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreatePaymentResponse>.SuccessAsOk(
                new CreatePaymentResponse(newPayment.Id, true, null));
        }


        private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber,
            string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
        {
            // Simulate external payment processing logic
            await Task.Delay(1000); 
            return (true, null); // Assume the payment was successful

            //return (false,"Payment failed due to insufficient funds."); // Simulate a failure case
        }
    }
}

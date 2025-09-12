using MediatR;
using MicroserviceDemo.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceDemo.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public static class GetAllPaymentsByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentsByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllPaymentsByUserIdQuery())).ToGenericResult())
                .WithName("get-all-payments-by-userid")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

            return group;
        }
    }
}

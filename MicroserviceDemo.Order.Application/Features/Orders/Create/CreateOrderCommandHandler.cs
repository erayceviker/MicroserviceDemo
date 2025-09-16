using MediatR;
using MicroserviceDemo.Order.Application.Contracts.IUnitOfWork;
using MicroserviceDemo.Order.Application.Contracts.Repositories;
using MicroserviceDemo.Order.Domain.Entities;
using MicroserviceDemo.Shared;
using MicroserviceDemo.Shared.Services;
using System.Net;
using MassTransit;
using MicroserviceDemo.Bus.Events;

namespace MicroserviceDemo.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(IIdentityService identityService,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint) 
        : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!request.Items.Any())
                return ServiceResult.Error("Order items not found", "Order must have at least one item",
                    HttpStatusCode.BadRequest);

            var newAddress = new Address
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };

            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.UserId, request.DiscountRate,
                newAddress.Id);

            foreach (var orderItem in request.Items)
                order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);

            order.Address = newAddress;

            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);


            // payment islemleri yapilacak

            var paymentId = Guid.Empty;

            order.SetPaidStatus(paymentId);

            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);

            await publishEndpoint.Publish(new OrderCreatedEvent(order.Id, identityService.UserId),cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
}

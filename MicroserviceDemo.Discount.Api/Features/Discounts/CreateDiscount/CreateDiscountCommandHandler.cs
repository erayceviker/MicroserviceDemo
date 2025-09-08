﻿using MicroserviceDemo.Discount.Api.Repositories;

namespace MicroserviceDemo.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context) : IRequestHandler<CreateDiscountCommand,ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var hasCodeForUser = await context.Discounts.AnyAsync(x => x.UserId == request.UserId && x.Code == request.Code,cancellationToken);

            if (hasCodeForUser)
            {
                return ServiceResult.Error("The same discount code cannot be used again.",HttpStatusCode.BadRequest);
            }

            var discount = new Discount()
            {
                Id = NewId.NextSequentialGuid(),
                Code = request.Code,
                Created = DateTime.Now,
                Rate = request.Rate,
                Expired = request.Expired,
                UserId = request.UserId
            };

            await context.Discounts.AddAsync(discount, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}

using AutoMapper;
using MediatR;
using Microservice.Demo.Basket.Api.Dtos;
using MicroserviceDemo.Shared;
using System.Net;
using System.Text.Json;

namespace Microservice.Demo.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(BasketService basketService, IMapper mapper) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket not found",HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            var basketDto = mapper.Map<BasketDto>(currentBasket);

            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}
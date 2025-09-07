

namespace MicroserviceDemo.Catalog.Api.Features.Categories.GetAll
{
    public record GetAllCategoryQuery : IRequestByServiceResult<List<CategoryDto>>;

    public class GetAllCategoryHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync(cancellationToken);

            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesDto);
        }
    }


    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult());
            return group;
        }
    }
}

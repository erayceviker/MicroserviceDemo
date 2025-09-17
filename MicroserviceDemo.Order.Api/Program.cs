using MicroserviceDemo.Bus;
using MicroserviceDemo.Order.Api.Endpoints.Orders;
using MicroserviceDemo.Order.Application;
using MicroserviceDemo.Order.Application.Contracts.IUnitOfWork;
using MicroserviceDemo.Order.Application.Contracts.Refit;
using MicroserviceDemo.Order.Application.Contracts.Refit.PaymentService;
using MicroserviceDemo.Order.Application.Contracts.Repositories;
using MicroserviceDemo.Order.Persistence;
using MicroserviceDemo.Order.Persistence.Repositories;
using MicroserviceDemo.Order.Persistence.UnitOfWork;
using MicroserviceDemo.Shared.Extensions;
using MicroserviceDemo.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

builder.Services.AddCommonMasstransitExt(builder.Configuration);

builder.Services.AddRefitConfigurationExt(builder.Configuration);
var app = builder.Build();

// endpoints
app.AddOrderGroupEndpointExt(app.AddVersionSetExt());


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();


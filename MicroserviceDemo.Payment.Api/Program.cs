using MicroserviceDemo.Payment.Api;
using MicroserviceDemo.Payment.Api.Feature.Payments;
using MicroserviceDemo.Payment.Api.Repositories;
using MicroserviceDemo.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("payment-in-memory-db"); });


builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddVersioningExt();


var app = builder.Build();

app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

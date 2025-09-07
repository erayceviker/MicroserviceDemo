using MicroserviceDemo.Catalog.Api.Options;
using MicroserviceDemo.Catalog.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddOptionsExtension();
builder.Services.AddRepositoryExtension();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();


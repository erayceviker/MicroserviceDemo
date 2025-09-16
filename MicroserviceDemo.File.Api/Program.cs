using MicroserviceDemo.Bus;
using MicroserviceDemo.File.Api;
using MicroserviceDemo.File.Api.Features.File;
using MicroserviceDemo.Shared.Extensions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddVersioningExt();


builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddMasstransitExt(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();

app.AddFileGroupEndpointExt(app.AddVersionSetExt());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
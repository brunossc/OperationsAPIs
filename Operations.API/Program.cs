using MassTransit;
using Serilog;
using Operations.API.Domain.Interfaces.Repositories;
using Operations.API.Domain.Interfaces.Services;
using Operations.API.Domain.Services;
using Operations.API.Application.DTO;
using Microsoft.Extensions.Configuration;
using Operations.API.Infrastructure.Repositories;
using Operations.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, services, configuration) =>
    configuration.WriteTo.Console().ReadFrom.Services(services));

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("OperationsDB"));

builder.Services.AddMassTransit(x =>
{
    var host = builder.Configuration.GetSection("MQconfiguration:HostName").Value;
    x.UsingRabbitMq((context, cfg) => cfg.Host(host));
});

//Services
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IOperationServiceApp, OperationServiceApp>();
builder.Services.AddScoped<IOperationService, OperationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/addoperation", async (OperationDTO operationDto, IOperationServiceApp operationService) =>
{
    await operationService.AddOperationAsync(operationDto);
    return Results.Ok();

}).WithOpenApi();

app.Run();
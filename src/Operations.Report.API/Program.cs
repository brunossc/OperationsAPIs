using MassTransit;
using MongoDB.Driver;
using Operations.Report.API.Application.Interfaces;
using Operations.Report.API.Application.Services;
using Operations.Report.API.Domain.Interfaces;
using Operations.Report.API.Domain.Services;
using Operations.Report.API.Infrastructure.Consumer;
using Operations.Report.API.Infrastructure.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient>(mc =>
{
    var settings = MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("NoSQLConnection"));
    settings.MaxConnectionPoolSize = 100;
    return new MongoClient(settings);
});

builder.Host.UseSerilog((context, services, configuration) =>
    configuration.WriteTo.Console().ReadFrom.Services(services));

builder.Services.AddScoped<IProcessedOperationRepository, ProcessedOperationRepository>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();

builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IOperationServiceApp, OperationServiceApp>();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OperationConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmqInstance", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("operation-queue", ep =>
        {
            ep.ConfigureConsumer<OperationConsumer>(context);
        });
    });
});
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.WriteTo.Console();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/getoperation", async (IOperationServiceApp operationService) =>
{
    var result = await operationService.GetAllAsync();
    return Results.Ok(result);

}).WithOpenApi();

app.Run();

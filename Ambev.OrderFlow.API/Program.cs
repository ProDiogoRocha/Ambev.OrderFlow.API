using Ambev.OrderFlow.API.Middlewares;
using Ambev.OrderFlow.Application.AppServices.Implementations;
using Ambev.OrderFlow.Application.AppServices.Interfaces;
using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Application.EventsHandlers;
using Ambev.OrderFlow.Application.Messaging;
using Ambev.OrderFlow.Application.Profiles;
using Ambev.OrderFlow.Domain.Aggregates;
using Ambev.OrderFlow.Domain.Entities;
using Ambev.OrderFlow.Domain.Interfaces;
using Ambev.OrderFlow.Infrastructure.Data.Configurations;
using Ambev.OrderFlow.Infrastructure.Data.Contexts;
using Ambev.OrderFlow.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RevendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<PedidoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetSection("RabbitMQ")
);

builder.Services.AddScoped<IRevendaRepository, RevendaRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

builder.Services.AddScoped<IHandlerService<Revenda, RevendaDTO>, RevendaHandlerService>();
builder.Services.AddScoped<IEmissorHandlerService, RevendaHandlerService>();
builder.Services.AddScoped<IHandlerService<Pedido, PedidoDTO>, PedidoHandlerService>();

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(LogEventHandler<>).Assembly));

builder.Services.AddScoped(typeof(INotificationHandler<>), typeof(LogEventHandler<>));

builder.Services.AddScoped<IMessageBus, RabbitMQProducer>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

// OpenTelemetry
// Jaeger Settings
var jaegerHost = builder.Configuration["Jaeger:AgentHost"] ?? "localhost";
var jaegerPort = int.Parse(builder.Configuration["Jaeger:AgentPort"] ?? "6831");

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddSource("Application.Events")
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("PedidoService"))
            .AddJaegerExporter(jaeger =>
            {
                jaeger.AgentHost = jaegerHost;
                jaeger.AgentPort = jaegerPort;
            });
    });


builder.Services.AddAutoMapper(typeof(RevendaProfile));
builder.Services.AddAutoMapper(typeof(PedidoProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<ExceptionNotificationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionNotificationMiddleware>();

app.Run();

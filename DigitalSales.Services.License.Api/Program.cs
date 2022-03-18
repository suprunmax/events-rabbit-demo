using Convey;
using Convey.MessageBrokers.CQRS;
using Convey.WebApi;
using DigitalSales.Service.License.Infrastructure;
using DigitalSales.Services.License.Application;
using DigitalSales.Services.License.Application.Events.External;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConvey()
    .AddWebApi()
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();
app.UseInfrastructure(s => s.SubscribeEvent<OrderCreated>());
app.Run();
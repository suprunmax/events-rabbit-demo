using Convey;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using DigitalSales.Services.Order.Application;
using DigitalSales.Services.Order.Application.Commands;
using DigitalSales.Services.Order.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConvey()
    .AddWebApi()
    .AddApplication()
    .AddInfrastructure()
    .Build();

var app = builder.Build();

app.UseInfrastructure();
app.UseDispatcherEndpoints(endpointsBuilder => endpointsBuilder
    .Post<CreateOrder>("orders")
);

app.Run();
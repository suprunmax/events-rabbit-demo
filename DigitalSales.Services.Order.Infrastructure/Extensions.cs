using Convey;
using Convey.CQRS.Events;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.RabbitMQ;
using Convey.WebApi;
using DigitalSales.Services.Order.Application.Commands;
using DigitalSales.Services.Order.Application.Services;
using DigitalSales.Services.Order.Infrastructure.Decorators;
using DigitalSales.Services.Order.Infrastructure.Exceptions;
using DigitalSales.Services.Order.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalSales.Services.Order.Infrastructure;

public static class Extensions
{
    public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
    {
        builder.Services.AddTransient<IMessageBroker, MessageBroker>();
        builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));

        builder.AddErrorHandler<ExceptionsResponseMapper>();
        builder.AddRabbitMq();
        builder.AddMessageOutbox(outbox => outbox.AddInMemory());
        
        return builder;
    }
    
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseErrorHandler()
            .UseConvey()
            .UseRabbitMq();
        
        return app;
    }
}

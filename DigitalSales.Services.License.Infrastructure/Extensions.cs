using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.RabbitMQ;
using Convey.WebApi;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.Outbox;
using DigitalSales.Service.License.Infrastructure.Decorators;
using DigitalSales.Service.License.Infrastructure.Exceptions;
using DigitalSales.Service.License.Infrastructure.Services;
using DigitalSales.Services.License.Application.Events.External;
using DigitalSales.Services.License.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalSales.Service.License.Infrastructure;

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
    
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, Action<IBusSubscriber> subscriber)
    {
        var bus = app.UseErrorHandler()
            .UseConvey()
            .UseRabbitMq();

        subscriber(bus);

        return app;
    }
}

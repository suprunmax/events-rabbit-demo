using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;

namespace DigitalSales.Services.Order.Application;

public static class Extensions
{
    public static IConveyBuilder AddApplication(this IConveyBuilder builder)
    {
        builder
            .AddCommandHandlers()
            .AddInMemoryCommandDispatcher()
            .AddEventHandlers()
            .AddInMemoryEventDispatcher();

        return builder;
    }
}
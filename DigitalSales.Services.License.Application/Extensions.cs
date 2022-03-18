using Convey;
using Convey.CQRS.Events;

namespace DigitalSales.Services.License.Application;

public static class Extensions
{
    public static IConveyBuilder AddApplication(this IConveyBuilder builder)
    {
        builder
            .AddEventHandlers()
            .AddInMemoryEventDispatcher();

        return builder;
    }
}
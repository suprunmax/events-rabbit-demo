using Convey.CQRS.Events;

namespace DigitalSales.Services.License.Application.Events.External.Handlers;

internal sealed class OrderCreatedHandler : IEventHandler<OrderCreated>
{
    public Task HandleAsync(OrderCreated @event, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.CompletedTask;
    }
}

// {
//     "OrderId": "79de4e82-8d2e-48a5-b942-ac3f686fa533"
// }
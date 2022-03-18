using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace DigitalSales.Services.License.Application.Events.External;

[Message("orders")]
public class OrderCreated : IEvent
{
    public Guid OrderId { get; }

    public OrderCreated(Guid orderId)
    {
        OrderId = orderId;
    }
}
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace DigitalSales.Services.Order.Application.Events;

[Message("orders")]
public class OrderCreated : IEvent
{
    public Guid OrderId { get; }

    public OrderCreated(Guid orderId)
    {
        OrderId = orderId;
    }
}
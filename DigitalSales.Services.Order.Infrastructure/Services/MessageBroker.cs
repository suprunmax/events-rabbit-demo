using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;
using DigitalSales.Services.Order.Application.Services;

namespace DigitalSales.Services.Order.Infrastructure.Services;

public class MessageBroker : IMessageBroker
{
    private readonly IBusPublisher _busPublisher;
    private readonly IMessageOutbox _messageOutbox;

    public MessageBroker(IBusPublisher busPublisher, IMessageOutbox messageOutbox)
    {
        _busPublisher = busPublisher;
        _messageOutbox = messageOutbox;
    }
        
    public Task PublishAsync(params IEvent[] events) => PublishAsync(events?.AsEnumerable());

    public async Task PublishAsync(IEnumerable<IEvent>? events)
    {
        if (events is null)
            return;

        foreach (var @event in events)
        {
            var messageId = Guid.NewGuid().ToString("N");

            if (_messageOutbox.Enabled)
            {
                await _messageOutbox.SendAsync(@event, messageId);
                continue;
            }
            
            await _busPublisher.PublishAsync(@event, messageId);
        }
    }
}
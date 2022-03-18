using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;

namespace DigitalSales.Services.Order.Infrastructure.Decorators;

public class OutboxEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _handler;
    private readonly IMessageOutbox _messageOutbox;
    private readonly bool _enabled;
    private readonly string _messageId;
    
    public OutboxEventHandlerDecorator(IEventHandler<T> handler, IMessageOutbox messageOutbox, IMessagePropertiesAccessor messagePropertiesAccessor)
    {
        _handler = handler;
        _messageOutbox = messageOutbox;
        _enabled = _messageOutbox.Enabled;
        
        var messageProps = messagePropertiesAccessor.MessageProperties;
        _messageId = string.IsNullOrWhiteSpace(messageProps?.MessageId) ? Guid.NewGuid().ToString("N") : messageProps.MessageId;
    }

    public Task HandleAsync(T @event, CancellationToken cancellationToken = new CancellationToken())
        => _enabled
            ? _messageOutbox.HandleAsync(_messageId, () => _handler.HandleAsync(@event, cancellationToken))
            : _handler.HandleAsync(@event, cancellationToken);
}
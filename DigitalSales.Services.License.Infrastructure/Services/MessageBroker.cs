using Convey.CQRS.Events;
using Convey.MessageBrokers;
using DigitalSales.Services.License.Application.Services;

namespace DigitalSales.Service.License.Infrastructure.Services;

public class MessageBroker : IMessageBroker
{
    private readonly IBusPublisher _busPublisher;

    public MessageBroker(IBusPublisher busPublisher)
    {
        _busPublisher = busPublisher;
    }
        
    public Task PublishAsync(params IEvent[] events) => PublishAsync(events?.AsEnumerable());

    public async Task PublishAsync(IEnumerable<IEvent>? events)
    {
        if (events is null)
            return;

        foreach (var @event in events)
        {
            var messageId = Guid.NewGuid().ToString("N");
            await _busPublisher.PublishAsync(@event, messageId);
        }
    }
}
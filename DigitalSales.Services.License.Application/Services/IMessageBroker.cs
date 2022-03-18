using Convey.CQRS.Events;

namespace DigitalSales.Services.License.Application.Services;

public interface IMessageBroker
{
    Task PublishAsync(params IEvent[] events);
    Task PublishAsync(IEnumerable<IEvent>? @event);
}
using Convey.CQRS.Commands;
using DigitalSales.Services.Order.Application.Events;
using DigitalSales.Services.Order.Application.Services;

namespace DigitalSales.Services.Order.Application.Commands.Handlers;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrder>
{
    private readonly IMessageBroker _messageBroker;

    public CreateOrderCommandHandler(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(CreateOrder command, CancellationToken cancellationToken = new CancellationToken())
    {
        //TODO: Create order code
        
        await _messageBroker.PublishAsync(new OrderCreated(Guid.Parse(command.Id)));
        
    }
}
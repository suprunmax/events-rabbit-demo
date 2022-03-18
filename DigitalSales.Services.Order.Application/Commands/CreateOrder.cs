using Convey.CQRS.Commands;

namespace DigitalSales.Services.Order.Application.Commands;

public class CreateOrder : ICommand
{
    public CreateOrder(string orderId)
    {
        Id = orderId;
    }

    public string Id { get; }
}
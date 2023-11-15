using FluentValidator;
using StackFlow.Domain.Commands;
using StackFlow.Domain.Commands.Stock.Inputs;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Repositories;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Handlers
{
  public class StockHandler : Notifiable, ICommandHandler<CreateStockCommand>
  {
    private readonly IStockRepository _repository;
    public StockHandler(IStockRepository repository)
    {
      _repository = repository;
    }
    public ICommandResult? Handle(CreateStockCommand command)
    {
      command.Validate();
      AddNotifications(command.Notifications);

      if (Invalid)
        return new CommandResult(
            false,
            "Please correct the fields below:",
            Notifications);

      if (_repository.CheckSymbol(command.Symbol))
      {
        AddNotification("Symbol", "There is already a stock with this symbol!");
      }

      if (Invalid)
        return new CommandResult(
            false,
            "Please fix below ",
            Notifications);

      var stock = new Stock(command.Name,
                            command.Symbol,
                            command.Price,
                            command.Sector,
                            command.AvailableQuantity);


      AddNotifications(stock.Notifications);

      if (Invalid)
        return new CommandResult(
            false,
            "Please correct the fields below:",
            Notifications);

      _repository.Save(stock);

      return new CommandResult(
            true,
            "Stock created successfully!",
            Notifications,
            stock.Id); ;
    }
  }
}
using FluentValidator;
using StackFlow.Domain.Commands;
using StackFlow.Domain.Commands.Transaction.Inputs;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Repositories;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Handlers
{
  public class TransactionHandler : Notifiable, ICommandHandler<CreateTransactionCommand>
  {
    private readonly ITransactionRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IStockRepository _stockRepository;
    public TransactionHandler(ITransactionRepository repository, IUserRepository userRepository, IStockRepository stockRepository)
    {
      _repository = repository;
      _userRepository = userRepository;
      _stockRepository = stockRepository;
    }

    public ICommandResult? Handle(CreateTransactionCommand command)
    {
      command.Validate();
      AddNotifications(command.Notifications);

      if (Invalid)
        return new CommandResult(
            false,
            "Please correct the fields below:",
            Notifications);

      var user = _userRepository.GetFullUser(command.UserId);
      var stock = _stockRepository.Get(command.StockId);

      if (user == null)
        AddNotification("UserId", "User not found!");

      if (stock.Id == Guid.Empty)
        AddNotification("StockId", "Stock not found!");

      if (Invalid)
        return new CommandResult(
            false,
            "Please correct the fields below:",
            Notifications);

      var fullStock = new Stock(stock);

      var transaction = new Transaction(command.Amount, fullStock, ETransactionType.Debit);



      return new CommandResult(
        true,
        "User created successfully!",
        Notifications,
        user.Id
      );
    }
  }
}
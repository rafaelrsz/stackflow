using FluentValidator;
using FluentValidator.Validation;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Commands.Transaction.Inputs
{
  public class CreateTransactionCommand : Notifiable, ICommand
  {
    public int Amount { get; set; }
    public Guid StockId { get; set; }
    public Guid UserId { get; set; }
    public ETransactionType TransactionType { get; set; }

    public bool Validate()
    {
      AddNotifications(new ValidationContract()
        .IsGreaterThan(Amount, 0, "Amount", "Invalid amount!")
        .AreNotEquals(StockId, Guid.Empty, "StockId", "Invalid StockId!")
        .AreNotEquals(UserId, Guid.Empty, "UserId", "Invalid StockId!")
      );

      return IsValid;
    }
  }
}
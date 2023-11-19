using FluentValidator.Validation;
using StackFlow.Shared.Entities;

namespace StackFlow.Domain.Entities
{
  public class Transaction : Entity
  {
    public Transaction(int amount, Stock stock, ETransactionType transactionType)
    {
      Date = DateTime.Now;
      Amount = amount;
      Stock = stock;
      TransactionType = transactionType;
      TotalPrice = amount * stock.Price;

      AddNotifications(new ValidationContract()
        .IsGreaterThan(Amount, 0, "Amount", "Invalid amount!")
        .IsGreaterThan(Date, DateTime.Now, "Date", "Invalid transaction date!")
      );
    }

    public DateTime Date { get; private set; }
    public int Amount { get; private set; }
    public double TotalPrice { get; private set; }
    public Stock Stock { get; private set; }
    public ETransactionType TransactionType { get; private set; }
  }
}
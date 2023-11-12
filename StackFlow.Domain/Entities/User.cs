using FluentValidator.Validation;
using StackFlow.Domain.ValueObjects;
using StackFlow.Shared.Entities;

namespace StackFlow.Domain.Entities
{
  public class User : Entity
  {
    private readonly IList<Transaction> _transactions;
    public User(string name, Email email, string password, double availableBalance, Document document)
    {
      Name = name;
      Email = email;
      Password = password;
      AvailableBalance = availableBalance;
      Document = document;
      _transactions = new List<Transaction>();

      AddNotifications(new ValidationContract()
        .HasMinLen(Name, 5, "Name", "Name must have at least 5 characters!")
      );
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public double AvailableBalance { get; private set; }
    public Document Document { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.ToArray();

    public void BuyStock(Transaction transaction)
    {
      var transactionPrice = transaction.Amount * transaction.Stock.Price;

      if (AvailableBalance < transactionPrice)
      {
        AddNotification("AvailableBalance", "Insufficient available balance!");
        return;
      }

      AvailableBalance -= transactionPrice;
      _transactions.Add(transaction);
    }

    public void SellStock(Transaction transaction)
    {
      if (!_transactions.Any(p => p.Stock == transaction.Stock))
      {
        AddNotification("Transaction", "It is not possible to sell a share that you do not own!");
        return;
      }

      var transactionPrice = transaction.Amount * transaction.Stock.Price;

      AvailableBalance += transactionPrice;
      _transactions.Add(transaction);
    }
  }
}
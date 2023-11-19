using FluentValidator.Validation;
using StackFlow.Domain.Enums;
using StackFlow.Domain.ValueObjects;
using StackFlow.Shared.Entities;
using Microsoft.AspNetCore.Identity;


namespace StackFlow.Domain.Entities
{
  public class User : Entity
  {
    private readonly IList<Transaction> _transactions;
    public User(string name, string password, Document document, ERole role)
    {
      Name = name;
      Password = password;
      AvailableBalance = 0;
      Document = document;
      _transactions = new List<Transaction>();
      Role = role;

      AddNotifications(new ValidationContract()
        .HasMinLen(Name, 5, "Name", "Name must have at least 5 characters!")
      );
    }

    public User(Guid id, string name, string password, decimal availableBalance, string document, int role)
    {
      Id = id;
      Name = name;
      Password = password;
      AvailableBalance = (double)availableBalance;
      Document = new Document(document);
      _transactions = new List<Transaction>();
      Role = (ERole)role;
    }

    public string Name { get; private set; }
    public string Password { get; private set; }
    public double AvailableBalance { get; private set; }
    public Document Document { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.ToArray();
    public ERole Role { get; private set; }

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
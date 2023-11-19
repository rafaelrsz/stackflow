using FluentValidator.Validation;
using StackFlow.Domain.Enums;
using StackFlow.Domain.Queries;
using StackFlow.Shared.Entities;

namespace StackFlow.Domain.Entities
{
  public class Stock : Entity
  {
    public Stock(string name, string symbol, double price, ESector sector, int availableQuantity)
    {
      Name = name;
      Symbol = symbol;
      Price = price;
      Sector = sector;
      AvailableQuantity = availableQuantity;

      AddNotifications(new ValidationContract()
        .IsGreaterThan(Price, 0, "Price", "Invalid price!")
        .IsGreaterOrEqualsThan(AvailableQuantity, 0, "AvailableQuantity", "Invalid available quantity!")
        .HasMaxLen(Symbol, 10, "Symbol", "Symbol must not have more than 10 characters!")
      );
    }

    public Stock(Guid id, string name, string symbol, double price, ESector sector, int availableQuantity)
    {
      Id = id;
      Name = name;
      Symbol = symbol;
      Price = price;
      Sector = sector;
      AvailableQuantity = availableQuantity;
    }

    public Stock(ListStockQueryResult result)
    {
      Name = result.Name;
      Symbol = result.Symbol;
      Price = result.Price;
      Sector = result.Sector;
      AvailableQuantity = result.AvailableQuantity;
      Id = result.Id;
    }

    public string Name { get; private set; }
    public string Symbol { get; private set; }
    public double Price { get; private set; }
    public ESector Sector { get; private set; }
    public int AvailableQuantity { get; private set; }
  }
}
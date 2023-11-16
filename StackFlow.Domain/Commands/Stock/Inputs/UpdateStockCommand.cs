using FluentValidator;
using FluentValidator.Validation;
using StackFlow.Domain.Enums;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Commands.Stock.Inputs
{
  public class UpdateStockCommand : Notifiable, ICommand
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Symbol { get; set; } = "";
    public double Price { get; set; }
    public ESector Sector { get; set; }
    public int AvailableQuantity { get; set; }

    public bool Validate()
    {
      AddNotifications(new ValidationContract()
        .IsGreaterThan(Price, 0, "Price", "Invalid price!")
        .IsGreaterOrEqualsThan(AvailableQuantity, 0, "AvailableQuantity", "Invalid available quantity!")
        .HasMaxLen(Symbol, 10, "Symbol", "Symbol must not have more than 10 characters!")
        .IsNotNullOrEmpty(Name, "Name", "Name is required!")
        .IsNotNull(Sector, "Sector", "Sector is required!")
        .AreNotEquals(Id, Guid.Empty, "Id", "Id is required!")
        );

      return base.IsValid;
    }
  }
}
using System.ComponentModel;
using FluentValidator;
using FluentValidator.Validation;
using StackFlow.Domain.Enums;
using StackFlow.Domain.ValueObjects;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Commands.User.Inputs
{
  public class DepositFoundsCommand : Notifiable, ICommand
  {
    public Guid? Id { get; set; }
    [DisplayName("Saldo")]
    public double Amount { get; set; }

    public bool Validate()
    {
      AddNotifications(new ValidationContract()
        .IsGreaterOrEqualsThan(Amount, 0, "Amount", "Quantidade deve ser maior que zero!")
      );

      return base.IsValid;
    }
  }
}
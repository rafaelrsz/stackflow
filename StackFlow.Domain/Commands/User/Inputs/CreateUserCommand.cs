using System.ComponentModel;
using FluentValidator;
using FluentValidator.Validation;
using StackFlow.Domain.Enums;
using StackFlow.Domain.ValueObjects;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Commands.User.Inputs
{
  public class CreateUserCommand : Notifiable, ICommand
  {
    [DisplayName("Nome")]
    public string Name { get; set; } = "";

    [DisplayName("Senha")]
    public string Password { get; set; } = "";

    [DisplayName("Documento")]
    public string Document { get; set; } = "";

    public bool Validate()
    {
      AddNotifications(new ValidationContract()
        .HasMinLen(Name, 5, "Name", "Name must have at least 5 characters!")
        .IsNotNullOrEmpty(Password, "Password", "Password is required!")
        .IsNotNullOrEmpty(Document, "Document", "Document is required!")
      );

      return base.IsValid;
    }
  }
}
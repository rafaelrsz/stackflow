using FluentValidator;
using FluentValidator.Validation;
using StackFlow.Domain.Enums;
using StackFlow.Domain.ValueObjects;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Commands.User.Inputs
{
  public class CreateUserCommand : Notifiable, ICommand
  {
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    public string Document { get; set; } = "";
    public ERole Role { get; set; }

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
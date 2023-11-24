using FluentValidator;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Commands.User.Inputs
{
  public class UserLoginCommand : Notifiable, ICommand
  {
    public string Document { get; set; } = "";
    public string Password { get; set; } = "";

    public bool Validate()
    {
      return true;
    }
  }
}
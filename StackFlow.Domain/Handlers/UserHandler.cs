using FluentValidator;
using StackFlow.Domain.Commands;
using StackFlow.Domain.Commands.User.Inputs;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Enums;
using StackFlow.Domain.Repositories;
using StackFlow.Domain.Utils;
using StackFlow.Domain.ValueObjects;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Handlers
{
  public class UserHandler : Notifiable,
   ICommandHandler<CreateUserCommand>,
   ICommandHandler<UpdateUserCommand>,
   ICommandHandler<UserLoginCommand>
  {
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _hasher;
    public UserHandler(IUserRepository repository, IPasswordHasher hasher)
    {
      _repository = repository;
      _hasher = hasher;
    }

    public ICommandResult? Handle(CreateUserCommand command)
    {
      command.Validate();
      AddNotifications(command.Notifications);

      if (Invalid)
        return new CommandResult(
            false,
            "Please correct the fields below:",
            Notifications);

      if (_repository.CheckDocument(command.Document))
      {
        AddNotification("Document", "There is already an user with this document!");
      }

      if (Invalid)
        return new CommandResult(
            false,
            "Please fix below ",
            Notifications);

      var document = new Document(command.Document);
      var user = new User(command.Name,
                          _hasher.Hash(command.Password),
                          document,
                          command.Role);

      AddNotifications(document.Notifications);
      AddNotifications(user.Notifications);

      if (Invalid)
        return new CommandResult(
            false,
            "Please fix below ",
            Notifications);

      _repository.Save(user);

      return new CommandResult(
        true,
        "User created successfully!",
        Notifications,
        user.Id
      );
    }

    public ICommandResult? Handle(UpdateUserCommand command)
    {
      command.Validate();
      AddNotifications(command.Notifications);

      if (Invalid)
        return new CommandResult(
            false,
            "Please correct the fields below:",
            Notifications);

      var entity = _repository.GetFullUser(command.Id);

      if (entity == null)
      {
        AddNotification("Id", "User not found!");
      }

      if (Invalid)
        return new CommandResult(
            false,
            "Please fix below ",
            Notifications);

      var document = new Document(command.Document);
      var user = new User(command.Name,
                          _hasher.Hash(command.Password),
                          document,
                          entity!.Role);

      AddNotifications(document.Notifications);
      AddNotifications(user.Notifications);

      if (Invalid)
        return new CommandResult(
            false,
            "Please fix below ",
            Notifications);

      _repository.Update(user);

      return new CommandResult(
        true,
        "User updated successfully!",
        Notifications,
        user.Id
      );
    }

    public ICommandResult? Handle(UserLoginCommand command)
    {
      var user = _repository.GetByDocument(command.Document);

      if (user == null)
        AddNotification("User", "User not found!");

      if (Invalid)
        return new CommandResult(
          false,
          "User not found!",
          Notifications,
          Guid.NewGuid()
        );

      var match = _hasher.Verify(user!.Password, command.Password);

      if (!match)
        AddNotification("Password", "Wrong password!");

      if (Invalid)
        return new CommandResult(
        false,
        "User not found!",
        Notifications,
        user!.Id
        );

      return new CommandResult(
      match,
      "User logged in!",
      Notifications,
      user.Id
    );
    }
  }
}
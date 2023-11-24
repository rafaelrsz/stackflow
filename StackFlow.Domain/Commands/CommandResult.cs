using FluentValidator;
using StackFlow.Shared.Commands;

namespace StackFlow.Domain.Commands
{
  public class CommandResult : ICommandResult
  {
    public CommandResult(bool success, string message, IReadOnlyCollection<Notification> notifications)
    {
      Success = success;
      Message = message;
      Notifications = notifications;
    }

    public CommandResult(bool success, string message, IReadOnlyCollection<Notification> notifications, Guid id)
    {
      Success = success;
      Message = message;
      Notifications = notifications;
      Id = id;
    }
    public CommandResult(bool success, string message, IReadOnlyCollection<Notification> notifications, Guid id, object entity)
    {
      Success = success;
      Message = message;
      Notifications = notifications;
      Id = id;
      Entity = entity;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public IReadOnlyCollection<Notification> Notifications { get; set; }
    public Guid Id { get; set; }
    public object? Entity { get; set; } = null;
  }
}
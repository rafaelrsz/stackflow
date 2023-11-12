using FluentValidator;

namespace StackFlow.Shared.Entities
{
  public class Entity : Notifiable
  {
    public Guid Id { get; private set; }
  }
}
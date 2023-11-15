using FluentValidator;

namespace StackFlow.Shared.Entities
{
  public class Entity : Notifiable
  {
    public Entity()
    {
      Id = Guid.NewGuid();
    }
    public Guid Id { get; protected set; }
  }
}
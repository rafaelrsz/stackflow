using System.Text.Json.Serialization;
using StackFlow.Domain.Enums;
using StackFlow.Domain.ValueObjects;

namespace StackFlow.Domain.Queries
{
  public class ListUserQueryResult
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    public double AvailableBalance { get; set; }
    public string Document { get; set; } = "";
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ERole Role { get; set; }
  }
}
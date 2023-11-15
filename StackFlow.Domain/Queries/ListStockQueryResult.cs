using System.Text.Json.Serialization;
using StackFlow.Domain.Enums;

namespace StackFlow.Domain.Queries
{
  public class ListStockQueryResult
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Symbol { get; set; } = "";
    public double Price { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ESector Sector { get; set; }
    public int AvailableQuantity { get; set; }
  }
}
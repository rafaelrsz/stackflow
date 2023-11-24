using System.ComponentModel;
using System.Text.Json.Serialization;
using StackFlow.Domain.Enums;

namespace StackFlow.Domain.Queries
{
  public class ListStockReportQueryResult
  {
    public Guid Id { get; set; }

    [DisplayName("Nome")]
    public string Name { get; set; } = "";

    [DisplayName("Sigla")]
    public string Symbol { get; set; } = "";

    [DisplayName("Preço")]
    public double Price { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [DisplayName("Setor")]
    public ESector Sector { get; set; }

    [DisplayName("Data da compra")]
    public DateTime Date { get; set; }
  }
}
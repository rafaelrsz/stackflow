using System.Text.Json.Serialization;
using StackFlow.Domain.Enums;

namespace StackFlow.Domain.Queries
{
  public class ListTransactionQueryResult
  {
    public DateTime Date { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }
    public Guid StockId { get; set; }
    public Guid UserId { get; set; }
    public ETransactionType TransactionType { get; set; }
  }
}
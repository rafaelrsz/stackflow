using StackFlow.Domain.Entities;
using StackFlow.Domain.Queries;

namespace StackFlow.Domain.Repositories
{
  public interface ITransactionRepository
  {
    IEnumerable<ListTransactionQueryResult> GetByUser(Guid userId);
    IEnumerable<ListStockReportQueryResult> GetTransactionsByUser(Guid userId);
    ListTransactionQueryResult Get(Guid id);
    IEnumerable<ListTransactionQueryResult> Get();
    void Save(Transaction transaction, Guid userId);
  }
}
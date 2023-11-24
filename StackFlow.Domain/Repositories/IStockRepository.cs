using StackFlow.Domain.Entities;
using StackFlow.Domain.Queries;

namespace StackFlow.Domain.Repositories
{
  public interface IStockRepository
  {
    bool CheckSymbol(string symbol);
    ListStockQueryResult Get(Guid id);
    IEnumerable<ListStockQueryResult> GetByUser(Guid userId);
    Stock? GetFullStock(Guid id);
    IEnumerable<ListStockQueryResult> Get();
    void Save(Stock stock);
    void Delete(Guid id);
    void Update(Stock stock);
    bool ValidateExclusion(Guid id);
  }
}
using System.Data;
using Dapper;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Queries;
using StackFlow.Domain.Repositories;
using StackFlow.Infra.DataContexts;

namespace StackFlow.Infra.Repositories
{
  public class StockRepository : IStockRepository
  {
    private readonly StackFlowDataContext _context;

    public StockRepository(StackFlowDataContext context)
    {
      _context = context;
    }
    public bool CheckSymbol(string symbol)
    {
      return
      _context
        .Connection
        .Query<bool>(
            "spCheckSymbol",
            new { symbol },
            commandType: CommandType.StoredProcedure)
        .FirstOrDefault();
    }

    public void Delete(Guid id)
    {
      _context
        .Connection
        .Query<bool>(
            "spDeleteStock",
            new
            {
              id,
            },
            commandType: CommandType.StoredProcedure)
        .FirstOrDefault();
    }

    public ListStockQueryResult Get(Guid id)
    {
      return
      _context
        .Connection
        .Query<ListStockQueryResult>(
            "spGetStock",
            new { id },
            commandType: CommandType.StoredProcedure)
        .FirstOrDefault() ?? new ListStockQueryResult();
    }

    public IEnumerable<ListStockQueryResult> Get()
    {
      return
      _context
        .Connection
        .Query<ListStockQueryResult>(
            "spGetAllStocks",
            commandType: CommandType.StoredProcedure);
    }

    public void Save(Stock stock)
    {
      _context
      .Connection
      .Query<bool>(
          "spCreateStock",
          new
          {
            stock.Id,
            stock.Name,
            stock.Price,
            stock.Sector,
            stock.AvailableQuantity,
            stock.Symbol
          },
          commandType: CommandType.StoredProcedure)
      .FirstOrDefault();
    }

    public void Update(Stock stock)
    {
      _context
        .Connection
        .Query(
            "spUpdateStock",
            new
            {
              stock.Id,
              stock.Name,
              stock.Price,
              stock.Sector,
              stock.AvailableQuantity,
              stock.Symbol
            },
            commandType: CommandType.StoredProcedure);
    }

    public bool ValidateExclusion(Guid id)
    {
      return
      _context
        .Connection
        .Query<bool>(
            "spValidateStockExclusion",
            new { id },
            commandType: CommandType.StoredProcedure)
            .FirstOrDefault();
    }
  }
}
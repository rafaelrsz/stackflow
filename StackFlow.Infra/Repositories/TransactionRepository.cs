using System.Data;
using Dapper;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Queries;
using StackFlow.Domain.Repositories;
using StackFlow.Infra.DataContexts;

namespace StackFlow.Infra.Repositories
{
  public class TransactionRepository : ITransactionRepository
  {
    private readonly StackFlowDataContext _context;

    public TransactionRepository(StackFlowDataContext context)
    {
      _context = context;
    }

    public ListTransactionQueryResult Get(Guid id)
    {
      return
      _context
        .Connection
        .QueryFirstOrDefault<ListTransactionQueryResult>(
            "spGetTransaction",
            new { id },
            commandType: CommandType.StoredProcedure)
        ?? new ListTransactionQueryResult();
    }

    public IEnumerable<ListTransactionQueryResult> Get()
    {
      return
      _context
        .Connection
        .Query<ListTransactionQueryResult>(
            "spGetAllTransactions",
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<ListTransactionQueryResult> GetByUser(Guid userId)
    {
      return
      _context
        .Connection
        .Query<ListTransactionQueryResult>(
            "spGetAllTransactions",
            new { userId = userId },
            commandType: CommandType.StoredProcedure);
    }

    public void Save(Transaction transaction, Guid userId)
    {
      _context
        .Connection
        .Query<bool>(
            "spInsertTransaction",
            new
            {
              transaction.Id,
              StockId = transaction.Stock.Id,
              transaction.TotalPrice,
              transaction.Amount,
              transaction.TransactionType,
              UserId = userId
            },
            commandType: CommandType.StoredProcedure)
        .FirstOrDefault();
    }
  }
}
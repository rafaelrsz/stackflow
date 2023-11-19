using Microsoft.AspNetCore.Mvc;
using StackFlow.Domain.Queries;
using StackFlow.Domain.Repositories;

namespace StackFlow.Api.Controllers
{
  [Route("transaction")]
  public class TransactionController : Controller
  {
    private readonly ITransactionRepository _repository;

    public TransactionController(ITransactionRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public IEnumerable<ListTransactionQueryResult> Get()
    {
      return _repository.Get();
    }

    [HttpGet]
    [Route("{id}")]
    public ListTransactionQueryResult Get(Guid id)
    {
      return _repository.Get(id);
    }

    [HttpPost]
    public IActionResult Post()
    {
      return null;
    }
  }
}
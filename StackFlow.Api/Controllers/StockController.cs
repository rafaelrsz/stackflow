using Microsoft.AspNetCore.Mvc;
using StackFlow.Domain.Commands;
using StackFlow.Domain.Commands.Stock.Inputs;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Handlers;
using StackFlow.Domain.Queries;
using StackFlow.Domain.Repositories;

namespace StackFlow.Api.Controllers
{
  [Route("stock")]
  public class StockController : Controller
  {
    private readonly IStockRepository _repository;
    private readonly StockHandler _handler;

    public StockController(IStockRepository repository, StockHandler handler)
    {
      _repository = repository;
      _handler = handler;
    }

    // [HttpGet]
    // [Route("{id}")]
    // public ListStockQueryResult Get(Guid id)
    // {
    //   return _repository.Get(id);
    // }

    [HttpGet]
    public IActionResult Get()
    {
      return View(_repository.Get());
    }
    [Route("login")]
    public IActionResult Login()
    {
      return View();
    }

    // [HttpDelete]
    // [Route("{id}")]
    // public IActionResult Delete(Guid id)
    // {
    //   if (_repository.Get(id) == null)
    //   {
    //     return NotFound("Stock not found!");
    //   }

    //   if (!_repository.ValidateExclusion(id))
    //   {
    //     return Conflict("User owns this share purchased! Deletion is not possible.");
    //   }

    //   _repository.Delete(id);
    //   return Ok();
    // }

    // [HttpPost]
    // public IActionResult Post([FromBody] CreateStockCommand command)
    // {
    //   var result = _handler.Handle(command) as CommandResult;

    //   if (_handler.Invalid || result is null)
    //     return BadRequest(_handler.Notifications);

    //   return Ok(result.Id);
    // }

    // [HttpPut]
    // public IActionResult Put([FromBody] UpdateStockCommand command)
    // {
    //   var result = _handler.Handle(command) as CommandResult;

    //   if (_handler.Invalid || result is null)
    //     return BadRequest(_handler.Notifications);

    //   return Ok(result.Id);
    // }
  }
}

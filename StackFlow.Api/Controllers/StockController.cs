using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using StackFlow.Domain.Commands;
using StackFlow.Domain.Commands.Stock.Inputs;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Enums;
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

    [HttpGet]
    [Route("userstocks")]
    public IActionResult GetByUser()
    {
      var id = Guid.Parse(HttpContext.Session.GetString("UserId"));
      return View(_repository.GetByUser(id));
    }

    [HttpPost]
    [Route("{id}"), ActionName("Delete")]
    public IActionResult Delete(Guid id)
    {
      if (_repository.Get(id) == null)
      {
        return NotFound("Stock not found!");
      }

      if (_repository.ValidateExclusion(id))
      {
        return Conflict("User owns this share purchased! Deletion is not possible.");
      }

      _repository.Delete(id);
      return RedirectToAction("Get");
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateStockCommand command)
    {
      var result = _handler.Handle(command) as CommandResult;

      if (_handler.Invalid || result is null)
        return BadRequest(_handler.Notifications);

      return Ok(result.Id);
    }

    [HttpPut]
    public IActionResult Put([FromBody] UpdateStockCommand command)
    {
      var result = _handler.Handle(command) as CommandResult;

      if (_handler.Invalid || result is null)
        return BadRequest(_handler.Notifications);

      return Ok(result.Id);
    }

    [Route("add")]
    public IActionResult Create(int id = 0)
    {
      ViewBag.Sectors = Enum.GetValues(typeof(ESector)).Cast<ESector>();
      return View();
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add(CreateStockCommand command)
    {
      var result = _handler.Handle(command) as CommandResult;
      if (result!.Success)
      {
        ViewBag.Success = true;
        return RedirectToAction("Get", "Stock");
      }
      else
      {
        ViewBag.Success = false;
        command.AddNotifications(result.Notifications);
        return View(command);
      }
    }

    [Route("update/{id}")]
    public IActionResult Update(Guid id)
    {
      ViewBag.Sectors = Enum.GetValues(typeof(ESector)).Cast<ESector>();
      var stock = _repository.Get(id);
      return View(new UpdateStockCommand()
      {
        Id = stock.Id,
        AvailableQuantity = stock.AvailableQuantity,
        Name = stock.Name,
        Price = stock.Price,
        Sector = stock.Sector,
        Symbol = stock.Symbol
      });
    }

    [HttpPost]
    [Route("update/{id}")]
    public IActionResult Update(UpdateStockCommand command)
    {
      var result = _handler.Handle(command) as CommandResult;
      if (result!.Success)
      {
        ViewBag.Success = true;
        return RedirectToAction("Get", "Stock");
      }
      else
      {
        ViewBag.Success = false;
        command.AddNotifications(result.Notifications);
        return View(command);
      }
    }



  }
}

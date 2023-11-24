using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Queries;
using StackFlow.Domain.Repositories;

namespace StackFlow.Api.Controllers
{
  [Route("transaction")]
  public class TransactionController : Controller
  {
    private readonly ITransactionRepository _repository;
    private readonly IStockRepository _stockRepository;
    private readonly IUserRepository _userRepository;

    public TransactionController(ITransactionRepository repository, IStockRepository stockRepository, IUserRepository userRepository)
    {
      _repository = repository;
      _stockRepository = stockRepository;
      _userRepository = userRepository;
    }

    [Route("{id}")]
    [HttpPost, ActionName("Buy")]
    public IActionResult Buy([FromRoute] Guid id)
    {
      var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
      var user = _userRepository.GetFullUser(userId);
      var stock1 = _stockRepository.Get(id);
      var stock = new Stock(stock1.Id, stock1.Name, stock1.Symbol, stock1.Price, stock1.Sector, stock1.AvailableQuantity);

      var transaction = new Transaction(1, stock, ETransactionType.Debit);

      user.BuyStock(transaction);
      stock.AvailableQuantity--;
      _repository.Save(transaction, userId);
      _userRepository.Update(user);
      _stockRepository.Update(stock);
      HttpContext.Session.SetString("UserFounds", user!.AvailableBalance.ToString());
      return RedirectToAction("Get", "Stock");
    }

    [Route("sell/{id}")]
    [HttpPost, ActionName("Sell")]
    public IActionResult Sell([FromRoute] Guid id)
    {
      var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
      var user = _userRepository.GetFullUser(userId);
      var stock1 = _stockRepository.Get(id);
      var stock = new Stock(stock1.Id, stock1.Name, stock1.Symbol, stock1.Price, stock1.Sector, stock1.AvailableQuantity);

      var transaction = new Transaction(1, stock, ETransactionType.Credit);

      user.SellStock(transaction);
      stock.AvailableQuantity++;
      _repository.Save(transaction, userId);
      _userRepository.Update(user);
      _stockRepository.Update(stock);
      HttpContext.Session.SetString("UserFounds", user!.AvailableBalance.ToString());
      return RedirectToAction("GetByUser", "Stock");
    }

    [HttpGet]
    [Route("report")]
    public IActionResult GetByUser()
    {
      var id = Guid.Parse(HttpContext.Session.GetString("UserId"));
      return new ViewAsPdf(_repository.GetTransactionsByUser(id));
    }

  }
}
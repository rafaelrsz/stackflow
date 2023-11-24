using Microsoft.AspNetCore.Mvc;
using StackFlow.Domain.Commands;
using StackFlow.Domain.Commands.User.Inputs;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Handlers;
using StackFlow.Domain.Queries;
using StackFlow.Domain.Repositories;

namespace StackFlow.Api.Controllers
{
  [Route("user")]
  public class UserController : Controller
  {
    private readonly IUserRepository _repository;
    private readonly UserHandler _handler;
    public UserController(IUserRepository repository, UserHandler handler)
    {
      _repository = repository;
      _handler = handler;
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateUserCommand command)
    {
      var result = _handler.Handle(command) as CommandResult;

      if (_handler.Invalid || result is null)
        return BadRequest(_handler.Notifications);

      return Ok(result.Id);
    }

    [HttpGet]
    public IActionResult Get()
    {
      return View(_repository.Get());
    }

    [HttpGet]
    [Route("{id}")]
    public ListUserQueryResult Get(Guid id)
    {
      return _repository.Get(id);
    }

    [HttpPost]
    [Route("{id}")]
    public IActionResult Delete(Guid id)
    {
      if (_repository.Get(id) == null)
      {
        return NotFound("User not found!");
      }

      if (!_repository.ValidateExclusion(id))
      {
        return Conflict("User has shares purchased! Deletion is not possible.");
      }

      _repository.Delete(id);
      return RedirectToAction("Get");
    }

    [HttpPut]
    public IActionResult Put(UpdateUserCommand command)
    {
      var result = _handler.Handle(command) as CommandResult;

      if (_handler.Invalid || result is null)
        return BadRequest(_handler.Notifications);

      return Ok(result.Id);
    }

    [HttpGet]
    [Route("/login")]
    public IActionResult Login() => View();
    [Route("/login")]

    public IActionResult Login(UserLoginCommand command)
    {
      if (String.IsNullOrEmpty(command.Document) || string.IsNullOrEmpty(command.Password))
      {
        ViewBag.Success = false;
        return View();
      }
      else
      {
        var result = _handler.Handle(command) as CommandResult;
        if (result!.Success)
        {
          var user = result.Entity as User;
          ViewBag.Success = true;
          HttpContext.Session.SetString("UserId", result!.Id.ToString());
          HttpContext.Session.SetString("UserName", user!.Name);
          HttpContext.Session.SetString("UserFounds", user!.AvailableBalance.ToString());
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

    [Route("/register")]
    public IActionResult Register() => View();

    [HttpPost]
    [Route("/register")]
    public IActionResult Register(CreateUserCommand command)
    {
      var result = _handler.Handle(command) as CommandResult;
      if (result!.Success)
      {
        ViewBag.Success = true;
        return RedirectToAction("Login", "User");
      }
      else
      {
        ViewBag.Success = false;
        command.AddNotifications(result.Notifications);
        return View(command);
      }
    }

    [Route("/update/{id}")]
    public IActionResult Update(Guid id)
    {
      var entity = _repository.GetFullUser(id);
      return View(new UpdateUserCommand()
      {
        Id = entity!.Id,
        Document = entity.Document.Number,
        Name = entity.Name,
        Password = entity.Password
      });
    }

    [HttpPost]
    [Route("/update/{id}")]
    public IActionResult Update(UpdateUserCommand command)
    {
      var result = _handler.Handle(command) as CommandResult;
      if (result!.Success)
      {
        ViewBag.Success = true;
        return RedirectToAction("Get");
      }
      else
      {
        ViewBag.Success = false;
        command.AddNotifications(result.Notifications);
        return View(command);
      }
    }

    [Route("/deposit")]
    public IActionResult Deposit() => View();

    [HttpPost]
    [Route("/deposit")]
    public IActionResult Deposit(DepositFoundsCommand command)
    {
      var id = command.Id = Guid.Parse(HttpContext.Session.GetString("UserId"));
      if (command.IsValid)
      {
        _repository.DepositFounds(command);
        ViewBag.Success = true;
        var user = _repository.Get(id.Value);
        HttpContext.Session.SetString("UserFounds", user.AvailableBalance.ToString());
        return View();
      }
      else
      {
        ViewBag.Success = false;
        command.AddNotifications(command.Notifications);
        return View(command);
      }
    }
  }
}
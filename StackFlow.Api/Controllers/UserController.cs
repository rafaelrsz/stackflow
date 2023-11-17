using Microsoft.AspNetCore.Mvc;
using StackFlow.Domain.Commands;
using StackFlow.Domain.Commands.User.Inputs;
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
    public IEnumerable<ListUserQueryResult> Get()
    {
      return _repository.Get();
    }

    [HttpGet]
    [Route("{id}")]
    public ListUserQueryResult Get(Guid id)
    {
      return _repository.Get(id);
    }

    [HttpDelete]
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
      return Ok();
    }

    [HttpPut]
    public IActionResult Put(UpdateUserCommand command)
    {
      var result = _handler.Handle(command) as CommandResult;

      if (_handler.Invalid || result is null)
        return BadRequest(_handler.Notifications);

      return Ok(result.Id);
    }
  }
}
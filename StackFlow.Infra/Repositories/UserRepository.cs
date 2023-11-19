using System.Data;
using Dapper;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Queries;
using StackFlow.Domain.Repositories;
using StackFlow.Infra.DataContexts;

namespace StackFlow.Infra.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly StackFlowDataContext _context;

    public UserRepository(StackFlowDataContext context)
    {
      _context = context;
    }
    public bool CheckDocument(string document)
    {
      return
      _context
        .Connection
        .QueryFirstOrDefault<bool>(
            "spCheckDocument",
            new { document },
            commandType: CommandType.StoredProcedure);
    }
    public void Delete(Guid id)
    {
      _context
      .Connection
      .Query<bool>(
          "spDeleteUser",
          new
          {
            id,
          },
          commandType: CommandType.StoredProcedure);
    }

    public ListUserQueryResult Get(Guid id)
    {
      return
      _context
        .Connection
        .QueryFirstOrDefault<ListUserQueryResult>(
            "spGetUser",
            new { id },
            commandType: CommandType.StoredProcedure)
        ?? new ListUserQueryResult();
    }

    public IEnumerable<ListUserQueryResult> Get()
    {
      return
      _context
        .Connection
        .Query<ListUserQueryResult>(
            "spGetAllUsers",
            commandType: CommandType.StoredProcedure);
    }

    public User? GetByDocument(string document)
    {
      return
      _context
        .Connection
        .Query<User>(
            "spGetUserByDocument",
            new { document },
            commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public User? GetFullUser(Guid id)
    {
      return
      _context
        .Connection
        .QueryFirstOrDefault<User>(
            "spGetUser",
            new { id },
            commandType: CommandType.StoredProcedure);
    }

    public void Save(User user)
    {
      _context
        .Connection
        .Query<bool>(
            "spCreateUser",
            new
            {
              user.Id,
              Document = user.Document.Number,
              user.Password,
              user.Name,
              user.Role,
              user.AvailableBalance,
            },
            commandType: CommandType.StoredProcedure);
    }

    public void Update(User user)
    {
      _context
        .Connection
        .Query<bool>(
            "spUpdateUser",
            new
            {
              user.Id,
              Document = user.Document.Number,
              user.Password,
              user.Name,
              user.Role,
              user.AvailableBalance,
            },
            commandType: CommandType.StoredProcedure);
    }

    public bool ValidateExclusion(Guid id)
    {
      return
      _context
        .Connection
        .QueryFirstOrDefault(
            "spValidateUserExclusion",
            new { id },
            commandType: CommandType.StoredProcedure);
    }
  }
}
using StackFlow.Domain.Commands.User.Inputs;
using StackFlow.Domain.Entities;
using StackFlow.Domain.Queries;

namespace StackFlow.Domain.Repositories
{
  public interface IUserRepository
  {
    bool CheckDocument(string document);
    ListUserQueryResult Get(Guid id);
    User? GetByDocument(string document);
    User? GetFullUser(Guid id);
    IEnumerable<ListUserQueryResult> Get();
    void Save(User user);
    void Delete(Guid id);
    void Update(User user);
    bool ValidateExclusion(Guid id);
    void DepositFounds(DepositFoundsCommand command);
  }
}
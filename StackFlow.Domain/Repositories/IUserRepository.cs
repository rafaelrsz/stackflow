using StackFlow.Domain.Entities;
using StackFlow.Domain.Queries;

namespace StackFlow.Domain.Repositories
{
  public interface IUserRepository
  {
    bool CheckDocument(string document);
    ListUserQueryResult Get(Guid id);
    User? GetFullUser(Guid id);
    IEnumerable<ListUserQueryResult> Get();
    void Save(User user);
    void Delete(Guid id);
    void Update(User user);
    bool ValidateExclusion(Guid id);
  }
}
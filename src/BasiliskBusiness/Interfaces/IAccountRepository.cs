using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interface;

public interface IAccountRepository
{
    Account Get(string username);
    void Insert(Account model);
    void Update(Account model);
}

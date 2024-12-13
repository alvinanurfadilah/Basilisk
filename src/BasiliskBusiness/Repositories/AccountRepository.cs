using BasiliskBusiness.Exceptions;
using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BasiliskTFContext _dbContext;

    public AccountRepository(BasiliskTFContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Account Get(string username)
    {
        return _dbContext.Accounts.Find(username) ?? throw new UsernamePasswordException("Username or Password is invalid");
    }

    public void Insert(Account model)
    {
        _dbContext.Accounts.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(Account model)
    {
        _dbContext.Accounts.Update(model);
        _dbContext.SaveChanges();
    }
}

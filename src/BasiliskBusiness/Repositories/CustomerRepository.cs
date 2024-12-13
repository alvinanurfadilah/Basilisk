using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BasiliskTFContext _dbContext;
    public CustomerRepository(BasiliskTFContext dbContext) {
        _dbContext = dbContext;
    }

    public List<Customer> Get()
    {
        return _dbContext.Customers.ToList();
    }
    public List<Customer> Get(int pageNumber, int pageSize, string companyName, string contactPerson)
    {
        return _dbContext.Customers
        .Where(customer => customer.CompanyName.ToLower().Contains(companyName??"".ToLower()) && customer.ContactPerson.ToLower().Contains(contactPerson??"".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Customer Get(long id)
    {
        return _dbContext.Customers.Find(id) ?? throw new NullReferenceException($"Customer with id {id} not found");
    }

    public int Count(string companyName, string contactPerson)
    {
        return _dbContext.Customers
        .Where(customer => customer.CompanyName.ToLower().Contains(companyName??"".ToLower()) && customer.ContactPerson.ToLower().Contains(contactPerson??"".ToLower())).Count();
    }

    public void Insert(Customer model)
    {
        _dbContext.Customers.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(Customer model)
    {
        _dbContext.Customers.Update(model);
        _dbContext.SaveChanges();
    }

    public void Delete(Customer model)
    {
        _dbContext.Customers.Remove(model);
        _dbContext.SaveChanges();
    }
}

using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interface;

public interface ICustomerRepository
{
    public List<Customer> Get();
    public List<Customer> Get(int pageNumber, int pageSize, string companyName, string contactPerson);
    public Customer Get(long id);
    public int Count(string companyName, string contactPerson);
    public void Insert(Customer model);
    public void Update(Customer model);
    public void Delete(Customer model);
}

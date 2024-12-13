using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interface;

public interface ISupplierRepository
{
    public List<Supplier> Get();
    public List<Supplier> Get(int pageNumber, int pageSize, string companyName, string contactPerson, string jobTitle);
    public Supplier Get(long id);
    public int Count(string companyName, string contactPerson, string jobTitle);
    public void Insert(Supplier model);
    public void Update(Supplier model);
}
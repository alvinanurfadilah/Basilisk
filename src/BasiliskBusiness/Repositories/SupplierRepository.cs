using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;

namespace BasiliskBusiness;

public class SupplierRepository : ISupplierRepository
{
    private readonly BasiliskTFContext _dbContext;
    public SupplierRepository(BasiliskTFContext dbContext) 
    {
        _dbContext = dbContext;
    }

    public List<Supplier> Get() 
    {
        return _dbContext.Suppliers.ToList();
    }

    public List<Supplier> Get(int pageNumber, int pageSize, string companyName, string contactPerson, string jobTitle)
    {
        return _dbContext.Suppliers
        .Where(supplier => supplier.CompanyName.ToLower().Contains(companyName??"".ToLower()) && supplier.ContactPerson.ToLower().Contains(contactPerson??"".ToLower()) && supplier.JobTitle.ToLower().Contains(jobTitle??"".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Supplier Get(long id) 
    {
        return _dbContext.Suppliers.Find(id) ?? throw new NullReferenceException($"Supplier with id {id} not found");
    }

    public int Count(string companyName, string contactPerson, string jobTitle) 
    {
        return _dbContext.Suppliers.Where(supplier => supplier.CompanyName.ToLower().Contains(companyName??"".ToLower()) && supplier.ContactPerson.ToLower().Contains(contactPerson??"".ToLower()) && supplier.JobTitle.ToLower().Contains(jobTitle??"".ToLower())).Count();
    }

    public void Insert(Supplier model)
    {
        _dbContext.Suppliers.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(Supplier model)
    {
        _dbContext.Suppliers.Update(model);
        _dbContext.SaveChanges();
    }
}
using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly BasiliskTFContext _dbContext;
    public DeliveryRepository(BasiliskTFContext dbContext) {
        _dbContext = dbContext;
    }

    public List<Delivery> Get()
    {
        return _dbContext.Deliveries.ToList();
    }
    public List<Delivery> Get(int pageNumber, int pageSize, string companyName)
    {
        return _dbContext.Deliveries
        .Where(delivery => delivery.CompanyName.ToLower().Contains(companyName??"".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Delivery Get(long id)
    {
        return _dbContext.Deliveries.Find(id) ?? throw new NullReferenceException($"Delivery with id {id} not found");
    }

    public int Count(string companyName)
    {
        return _dbContext.Deliveries
        .Where(delivery => delivery.CompanyName.ToLower().Contains(companyName??"".ToLower())).Count();
    }

    public void Insert(Delivery model)
    {
        _dbContext.Deliveries.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(Delivery model)
    {
        _dbContext.Deliveries.Update(model);
        _dbContext.SaveChanges();
    }

    public void Delete(Delivery model)
    {
        _dbContext.Deliveries.Remove(model);
        _dbContext.SaveChanges();
    }
}

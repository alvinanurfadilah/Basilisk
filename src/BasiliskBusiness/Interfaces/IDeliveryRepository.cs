using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interface;

public interface IDeliveryRepository
{
    public List<Delivery> Get();
    public List<Delivery> Get(int pageNumber, int pageSize, string companyName);
    public Delivery Get(long id);
    public int Count(string companyName);
    public void Insert(Delivery model);
    public void Update(Delivery model);
    public void Delete(Delivery model);
}
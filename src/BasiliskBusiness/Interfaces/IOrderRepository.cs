using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interface;

public interface IOrderRepository
{
    public List<Order> Get();
    public List<Order> Get(int pageNumber, int pageSize, string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate);
    public Order Get(string invoiceNumber);
    public int Count(string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate);
    public void Insert(Order model);
    public void Update(Order model);
    public void Delete(Order model);
    public decimal GetCost(long deliveryId);


    Order GetOrderDetail(string invoiceNumber);
}

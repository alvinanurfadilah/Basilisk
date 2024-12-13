using System;
using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface IOrderDetailRepository
{
    List<OrderDetail> Get(int pageNumber, int pageSize, string invoiceNumber);
    OrderDetail Get(long id);
    void Insert(OrderDetail model);
    void Update(OrderDetail model);
    void Delete(OrderDetail model);
    int Count(string invoiceNumber);
}

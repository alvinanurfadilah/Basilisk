using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BasiliskBusiness.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly BasiliskTFContext _dbContext;
    public OrderRepository(BasiliskTFContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Order> Get()
    {
        return _dbContext.Orders.ToList();
    }

    public List<Order> Get(int pageNumber, int pageSize, string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate)
    {
        return _dbContext.Orders
        .Include(order => order.Customer)
        .Include(order => order.SalesEmployeeNumberNavigation)
        .Include(order => order.Delivery)
        .Where(order => order.InvoiceNumber.Contains(invoiceNumber ?? "")
        && (order.Customer.Id == customerId || 0 == customerId)
        && order.SalesEmployeeNumberNavigation.EmployeeNumber.Contains(salesEmployeeNumber ?? "")
        && (order.Delivery.Id == deliveryId || 0 == deliveryId)
        && (order.OrderDate == orderDate || new DateTime() == orderDate))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Order Get(string invoiceNumber)
    {
        return _dbContext.Orders.Find(invoiceNumber) ?? throw new NullReferenceException($"Order with invoice number {invoiceNumber} not found");
    }

    public int Count(string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate)
    {
        return _dbContext.Orders
        .Where(order => order.InvoiceNumber.Contains(invoiceNumber ?? "")
        && (order.Customer.Id == customerId || 0 == customerId)
        && order.SalesEmployeeNumberNavigation.EmployeeNumber.Contains(salesEmployeeNumber ?? "")
        && (order.Delivery.Id == deliveryId || 0 == deliveryId)
        && (order.OrderDate == orderDate || new DateTime() == orderDate)).Count();
    }

    public void Insert(Order model)
    {
        _dbContext.Orders.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(Order model)
    {
        _dbContext.Orders.Update(model);
        _dbContext.SaveChanges();
    }

    public void Delete(Order model)
    {
        _dbContext.Orders.Remove(model);
        _dbContext.SaveChanges();
    }

    public decimal GetCost(long deliveryId)
    {
        return _dbContext.Orders
        .Include(order => order.Delivery)
        .Where(order => order.Delivery.Id == deliveryId)
        .Select(order => order.Delivery.Cost)
        .FirstOrDefault();
    }


    public Order GetOrderDetail(string invoiceNumber)
    {
        return _dbContext.Orders
        .Include(order => order.Customer)
        .Include(order => order.SalesEmployeeNumberNavigation)
        .Include(order => order.OrderDetails)
        .ThenInclude(od => od.Product)
        .Where(order => order.InvoiceNumber == invoiceNumber)
        .FirstOrDefault();
    }
}
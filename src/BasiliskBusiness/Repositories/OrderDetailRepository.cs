using System;
using BasiliskBusiness.Interfaces;
using BasiliskDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BasiliskBusiness.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly BasiliskTFContext _dbContext;

    public OrderDetailRepository(BasiliskTFContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<OrderDetail> Get(int pageNumber, int pageSize, string invoiceNumber)
    {
        return _dbContext.OrderDetails
        .Include(od => od.Product)
        .Include(od => od.InvoiceNumberNavigation)
        .ThenInclude(o => o.Customer)
        .Include(od => od.InvoiceNumberNavigation)
        .ThenInclude(o => o.SalesEmployeeNumberNavigation)
        .Where(od => od.InvoiceNumber == invoiceNumber)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public OrderDetail Get(long id)
    {
        return _dbContext.OrderDetails.Find(id);
    }

    public void Insert(OrderDetail model)
    {
        _dbContext.OrderDetails.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(OrderDetail model)
    {
        _dbContext.OrderDetails.Update(model);
        _dbContext.SaveChanges();
    }

    public void Delete(OrderDetail model)
    {
        _dbContext.OrderDetails.Remove(model);
        _dbContext.SaveChanges();
    }

    public int Count(string invoiceNumber)
    {
        return _dbContext.OrderDetails
        .Include(od => od.Product)
        .Include(od => od.InvoiceNumberNavigation)
        .ThenInclude(o => o.Customer)
        .Include(od => od.InvoiceNumberNavigation)
        .ThenInclude(o => o.SalesEmployeeNumberNavigation)
        .Where(od => od.InvoiceNumber == invoiceNumber)
        .Count();
    }
}

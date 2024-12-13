using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BasiliskBusiness;

public class ProductRepository : IProductRepository
{
    private readonly BasiliskTFContext _dbContext;

    public ProductRepository(BasiliskTFContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Product> Get()
    {
        return _dbContext.Products.ToList();
    }
    public List<Product> Get(int pageNumber, int pageSize, string name, long categoryId, long supplierId)
    {
        return _dbContext.Products
        .Include(product => product.Category)
        .Include(product => product.Supplier)
        .Where(product => product.Name.ToLower().Contains(name ?? "".ToLower()) && (product.Category.Id == categoryId || 0 == categoryId) && (product.Supplier.Id == supplierId || 0 == supplierId))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Product Get(long id)
    {
        return _dbContext.Products.Find(id) ?? throw new NullReferenceException($"Product with id {id} not found");
    }

    public int Count(string name, long categoryId, long supplierId)
    {
        return _dbContext.Products
        .Where(product => product.Name.ToLower().Contains(name ?? "".ToLower()) && (product.Category.Id == categoryId || 0 == categoryId) && (product.Supplier.Id == supplierId || 0 == supplierId)).Count();
    }

    public void Insert(Product model)
    {
        _dbContext.Products.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(Product model)
    {
        _dbContext.Products.Update(model);
        _dbContext.SaveChanges();
    }

    public void Delete(Product model)
    {
        _dbContext.Products.Remove(model);
        _dbContext.SaveChanges();
    }
}
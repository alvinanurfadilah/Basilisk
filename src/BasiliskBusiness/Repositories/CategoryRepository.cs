using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BasiliskBusiness.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly BasiliskTFContext _dbContext;

    public CategoryRepository(BasiliskTFContext dbContext) {
        _dbContext = dbContext;
    }

    public List<Category> Get()
    {
        return _dbContext.Categories.ToList();
    }

    public List<Category> Get(int pageNumber, int pageSize, string name)
    {
        return _dbContext.Categories
        .Include(category => category.Products)
        .Where(category => category.Name.ToLower().Contains(name??"".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Category Get(long id)
    {
        return _dbContext.Categories.Find(id) ?? throw new NullReferenceException($"Category with id {id} not found");
    }

    public int Count(string name)
    {
        return _dbContext.Categories
        .Where(category => category.Name.ToLower().Contains(name??"".ToLower()))
        .Count();
    }

    public void Insert(Category model)
    {
        _dbContext.Categories.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(Category model)
    {
        _dbContext.Categories.Update(model);
        _dbContext.SaveChanges();
    }

    public void Delete(Category model)
    {
        _dbContext.Categories.Remove(model);
        _dbContext.SaveChanges();
    }
}

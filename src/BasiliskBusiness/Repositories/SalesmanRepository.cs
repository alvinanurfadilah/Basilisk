using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BasiliskBusiness.Repositories;

public class SalesmanRepository : ISalesmanRepository
{
    private readonly BasiliskTFContext _dbContext;
    public SalesmanRepository(BasiliskTFContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Salesman> Get()
    {
        return _dbContext.Salesmen.ToList();
    }

    public List<Salesman> Get(int pageNumber, int pageSize, string employeeNumber, string fullName, string level, string superiorName)
    {
        return _dbContext.Salesmen
        .Where(
            sal => sal.EmployeeNumber.ToLower().Contains(employeeNumber??"".ToLower()) 
            && (sal.FirstName.ToLower() + sal.LastName.ToLower()).Contains(fullName??"".ToLower()) 
            && sal.Level.Contains(level??"")
            && (sal.SuperiorEmployeeNumberNavigation.FirstName.ToLower() + sal.SuperiorEmployeeNumberNavigation.LastName.ToLower()).Contains(superiorName??"".ToLower())
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int Count(string employeeNumber, string fullName, string level, string superiorName)
    {
        return _dbContext.Salesmen
        .Where(
            sal => sal.EmployeeNumber.ToLower().Contains(employeeNumber??"".ToLower()) 
            && (sal.FirstName.ToLower() + sal.LastName.ToLower()).Contains(fullName??"".ToLower()) 
            && sal.Level.Contains(level??"")
            && (sal.SuperiorEmployeeNumberNavigation.FirstName.ToLower() + sal.SuperiorEmployeeNumberNavigation.LastName.ToLower()).Contains(superiorName??"".ToLower())
        )
        .Count();
    }

    public Salesman Get(string employeeNumber)
    {
        return _dbContext.Salesmen.Find(employeeNumber) ?? throw new NullReferenceException($"Salesman with employee number {employeeNumber} not found");
    }

    public void Insert(Salesman model)
    {
        _dbContext.Salesmen.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(Salesman model)
    {
        _dbContext.Salesmen.Update(model);
        _dbContext.SaveChanges();
    }

    public void Delete(Salesman model)
    {
        _dbContext.Salesmen.Remove(model);
        _dbContext.SaveChanges();
    }


    public Salesman GetSalesmanRegion(string employeeNumber)
    {
        return _dbContext.Salesmen
        .Include(sal => sal.Regions)
        .Where(sal => sal.EmployeeNumber == employeeNumber).FirstOrDefault();
    }

    public void InsertSalesmanRegion(Salesman model)
    {
        try
        {
            _dbContext.Salesmen.Update(model);
            _dbContext.SaveChanges();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void DeleteSalesmanRegion(Salesman model)
    {
        try
        {
            _dbContext.Salesmen.Update(model);
            _dbContext.SaveChanges();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
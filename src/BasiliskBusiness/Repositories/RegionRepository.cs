using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BasiliskBusiness;

public class RegionRepository : IRegionRepository
{
    private readonly BasiliskTFContext _dbContext;

    public RegionRepository(BasiliskTFContext dbContext) {
        _dbContext = dbContext;
    }

    public List<Region> Get(int pageNumber, int pageSize, string city)
    {
        return _dbContext.Regions
        .Where(region => region.City.ToLower().Contains(city??"".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Region Get(long id)
    {
        return _dbContext.Regions.Find(id);
    }

    public Region GetRegionSalesman(long id, string employeeNumber, string fullName, string level, string superiorName)
    {
        return _dbContext.Regions
        .Include(region => region.SalesmanEmployeeNumbers)
        .Where(region => region.Id == id || 0 == id
        && region.SalesmanEmployeeNumbers.Any(salesman => salesman.EmployeeNumber.Contains(employeeNumber??""))
        && region.SalesmanEmployeeNumbers.Any(salesman => (salesman.FirstName + salesman.LastName).Contains(fullName??""))
        && region.SalesmanEmployeeNumbers.Any(salesman => salesman.Level.Contains(level??""))
        && region.SalesmanEmployeeNumbers.Any(salesman => (salesman.SuperiorEmployeeNumberNavigation.FirstName + salesman.SuperiorEmployeeNumberNavigation.LastName).Contains(superiorName??""))
        ).FirstOrDefault();
    }

    public int Count(string city)
    {
        return _dbContext.Regions.Where(region => region.City.ToLower().Contains(city??"".ToLower())).Count();
    }

    public Region InsertResultId(Region model)
    {
        _dbContext.Regions.Add(model);
        _dbContext.SaveChanges();
        return model;
    }

    public Region UpdateResultId(Region model)
    {
        _dbContext.Regions.Update(model);
        _dbContext.SaveChanges();
        return model;
    }

    public void Delete(Region model)
    {
        _dbContext.Regions.Remove(model);
        _dbContext.SaveChanges();
    }

    public int Count(long id, string employeeNumber, string fullName, string level, string superiorName)
    {
        return _dbContext.Salesmen
        .Include(salesman => salesman.Regions)
        .Where(salesman => id == 0 || salesman.Regions.Any(region => region.Id == id)
        && salesman.EmployeeNumber.Contains(employeeNumber??"")
        && (salesman.FirstName + salesman.LastName).Contains(fullName??"") 
        && salesman.Level.Contains(level??"") 
        && (salesman.SuperiorEmployeeNumberNavigation.FirstName + salesman.SuperiorEmployeeNumberNavigation.LastName).Contains(superiorName))
        .Count();
    }

    public void InsertRegionSalesman(Region model)
    {
        try
        {
            _dbContext.Regions.Update(model);
            _dbContext.SaveChanges();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public Region GetRegionSalesman(long id)
    {
        return _dbContext.Regions
        .Include(region => region.SalesmanEmployeeNumbers)
        .Where(region => region.Id == id || 0 == id).FirstOrDefault();
    }

    public void DeleteRegionSalesman(Region model)
    {
        try
        {
            _dbContext.Regions.Update(model);
            _dbContext.SaveChanges();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public List<Region> Get()
    {
        return _dbContext.Regions.ToList();
    }
}
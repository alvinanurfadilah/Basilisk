using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interface;

public interface IRegionRepository
{
    List<Region> Get();
    public List<Region> Get(int pageNumber, int pageSize, string city);
    public int Count(string city);
    public Region Get(long id);
    public Region InsertResultId(Region model);
    public Region UpdateResultId(Region model);
    public void Delete(Region model);

    // REGION SALESMAN
    public Region GetRegionSalesman(long id, string employeeNumber, string fullName, string level, string superiorName);
    public int Count(long id, string employeeNumber, string fullName, string level, string superiorName); 

    void InsertRegionSalesman(Region model);
    void DeleteRegionSalesman(Region model);
    Region GetRegionSalesman(long id);
}
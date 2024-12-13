using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interface;

public interface ISalesmanRepository
{
    List<Salesman> Get();
    List<Salesman> Get(int pageNumber, int pageSize, string employeeNumber, string fullName, string level, string superiorName);
    int Count(string employeeNumber, string fullName, string level, string superiorName);
    Salesman Get(string employeeNumber);
    void Insert(Salesman model);
    void Update(Salesman model);
    void Delete(Salesman model);

    // SALESMAN REGION
    Salesman GetSalesmanRegion(string employeeNumber);
    void InsertSalesmanRegion(Salesman model);
    void DeleteSalesmanRegion(Salesman model);
}

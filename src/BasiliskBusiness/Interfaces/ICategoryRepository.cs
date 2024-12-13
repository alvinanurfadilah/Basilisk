using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interface;

public interface ICategoryRepository
{
    public List<Category> Get();
    public List<Category> Get(int pageNumber, int pageSize, string name);
    public Category Get(long id);
    public int Count(string name);
    public void Insert(Category model);
    public void Update(Category model);
    public void Delete(Category model);
}
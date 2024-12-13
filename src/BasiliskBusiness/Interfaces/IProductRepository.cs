using BasiliskDataAccess.Models;

namespace BasiliskBusiness.Interface;

public interface IProductRepository
{
    public List<Product> Get();
    public List<Product> Get(int pageNumber, int pageSize, string productName, long categoryId, long supplierId);
    public Product Get(long id);
    public int Count(string productName, long categoryId, long supplierId);
    public void Insert(Product model);
    public void Update(Product model);
    public void Delete(Product model);
}
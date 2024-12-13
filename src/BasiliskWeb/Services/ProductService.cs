using System.Globalization;
using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using BasiliskWeb.ViewModels;
using BasiliskWeb.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.Services;

public class ProductService
{
    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISupplierRepository _supplierRepository;

    public ProductService(IProductRepository repository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository) {
        _repository = repository;
        _categoryRepository = categoryRepository;
        _supplierRepository = supplierRepository;
    }

    private List<SelectListItem> GetCategories() {
        var model = _categoryRepository.Get()
            .Select(cat => new SelectListItem() {
                Text = cat.Name,
                Value = cat.Id.ToString()
            }).ToList();

        return model;
    }

    private List<SelectListItem> GetSuppliers()
    {
        var model = _supplierRepository.Get()
        .Select(sup => new SelectListItem() {
            Text = sup.CompanyName,
            Value = sup.Id.ToString()
        }).ToList();

        return model;
    }

    public ProductIndexViewModel Get(int pageNumber, int pageSize, string name, long categoryId, long supplierId) {
        var model = _repository.Get(pageNumber, pageSize, name, categoryId, supplierId)
        .Select(pro => new ProductViewModel{
            Id = pro.Id,
            Name = pro.Name,
            Price = pro.Price,
            CategoryName = pro.Category.Name,
            CompanyName = pro.Supplier.CompanyName
        });

        return new ProductIndexViewModel() {
            Products = model.ToList(),
            Pagination = new PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count(name, categoryId, supplierId)
            },
            Name = name,
            CategoryId = categoryId,
            SupplierId = supplierId,
            Categories = GetCategories(),
            Suppliers = GetSuppliers()
        };
    }

    public ProductInsertUpdateViewModel Get()
    {
        return new ProductInsertUpdateViewModel()
        {
            Categories = GetCategories(),
            Suppliers = GetSuppliers()
        };
    }

    public ProductInsertUpdateViewModel Get(long id)
    {
        var model = _repository.Get(id);

        return new ProductInsertUpdateViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            CategoryId = model.CategoryId,
            SupplierId = model.SupplierId,
            Price = model.Price,
            Stock = model.Stock,
            OnOrder = model.OnOrder,
            Discontinue = model.Discontinue,
            Description = model.Description,
            Categories = GetCategories(),
            Suppliers = GetSuppliers()
        };
    }

    public void Insert(ProductInsertUpdateViewModel viewModel)
    {
        var model = new Product()
        {
            Name = viewModel.Name,
            CategoryId = viewModel.CategoryId,
            SupplierId = viewModel.SupplierId,
            Price = viewModel.Price,
            Stock = viewModel.Stock,
            OnOrder = viewModel.OnOrder,
            Discontinue = viewModel.Discontinue,
            Description = viewModel.Description
        };

        _repository.Insert(model);
    }

    public void Update(ProductInsertUpdateViewModel viewModel)
    {
        var model = new Product()
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            CategoryId = viewModel.CategoryId,
            SupplierId = viewModel.SupplierId,
            Price = viewModel.Price,
            Stock = viewModel.Stock,
            OnOrder = viewModel.OnOrder,
            Discontinue = viewModel.Discontinue,
            Description = viewModel.Description
        };

        _repository.Update(model);
    } 

    public void Delete(long id)
    {
        var model = _repository.Get(id);
        _repository.Delete(model);
    }
}
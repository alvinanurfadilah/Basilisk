using BasiliskBusiness;
using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using BasiliskWeb.ViewModels;
using BasiliskWeb.ViewModels.Supplier;

namespace BasiliskWeb.Services;

public class SupplierService
{
    private readonly ISupplierRepository _repository;

    public SupplierService(ISupplierRepository repository) {
        _repository = repository;
    }

    public SupplierIndexViewModel Get(int pageNumber, int pageSize, string companyName, string contactPerson, string jobTitle) {
        var model = _repository.Get(pageNumber, pageSize, companyName, contactPerson, jobTitle)
        .Select(sup => new SupplierViewModel{
            Id = sup.Id,
            CompanyName = sup.CompanyName,
            ContactPerson = sup.ContactPerson,
            JobTitle = sup.JobTitle,
            DeleteDate = sup.DeleteDate
        });

        return new SupplierIndexViewModel(){
            Suppliers = model.ToList(),
            Pagination = new PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count(companyName, contactPerson, jobTitle)
            },
            CompanyName = companyName,
            ContactPerson = contactPerson,
            JobTitle = jobTitle
        };
    }

    public SupplierInsertUpdateViewModel Get(long id)
    {
        var model = _repository.Get(id);

        return new SupplierInsertUpdateViewModel() {
            Id = model.Id,
            CompanyName = model.CompanyName,
            ContactPerson = model.ContactPerson,
            JobTitle = model.JobTitle,
            Address = model.Address,
            City = model.City,
            Phone = model.Phone,
            Email = model.Email
        };
    }

    public void Insert(SupplierInsertUpdateViewModel viewModel)
    {
        var model = new Supplier() {
            CompanyName = viewModel.CompanyName,
            ContactPerson = viewModel.ContactPerson,
            JobTitle = viewModel.JobTitle,
            Address = viewModel.Address,
            City = viewModel.City,
            Phone = viewModel.Phone,
            Email = viewModel.Email
        };

        _repository.Insert(model);
    } 

    public void Update(SupplierInsertUpdateViewModel viewModel)
    {
        var model = new Supplier() {
            Id = viewModel.Id,
            CompanyName = viewModel.CompanyName,
            ContactPerson = viewModel.ContactPerson,
            JobTitle = viewModel.JobTitle,
            Address = viewModel.Address,
            City = viewModel.City,
            Phone = viewModel.Phone,
            Email = viewModel.Email
        };
        _repository.Update(model);
    }

    public void Update(long id)
    {
        var model = _repository.Get(id);
        model.DeleteDate = DateTime.Now;
        _repository.Update(model);
    }
}
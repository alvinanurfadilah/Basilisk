using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using BasiliskWeb.ViewModels;
using BasiliskWeb.ViewModels.Customer;

namespace BasiliskWeb.Services;

public class CustomerService
{
    private readonly ICustomerRepository _repository;
    public CustomerService(ICustomerRepository repository) {
        _repository = repository;
    }

    public CustomerIndexViewModel Get(int pageNumber, int pageSize, string companyName, string contactPerson) {
        var model = _repository.Get(pageNumber, pageSize, companyName, contactPerson)
        .Select(cus => new CustomerViewModel{
            Id = cus.Id,
            CompanyName = cus.CompanyName,
            ContactPerson = cus.ContactPerson,
            Address = cus.Address,
            City = cus.City,
            DeleteDate = cus.DeleteDate
        });

        return new CustomerIndexViewModel() {
            Customers = model.ToList(),
            Pagination = new PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count(companyName, contactPerson)
            },
            CompanyName = companyName,
            ContactPerson = contactPerson
        };
    }

    public CustomerInsertUpdateViewModel Get(long id)
    {
        var model = _repository.Get(id);

        return new CustomerInsertUpdateViewModel()
        {
            Id = model.Id,
            CompanyName = model.CompanyName,
            ContactPerson = model.ContactPerson,
            Address = model.Address,
            City = model.City,
            Phone = model.Phone,
            Email = model.Email
        };
    }

    public void Insert(CustomerInsertUpdateViewModel viewModel)
    {
        var model = new Customer()
        {
            CompanyName = viewModel.CompanyName,
            ContactPerson = viewModel.ContactPerson,
            Address = viewModel.Address,
            City = viewModel.City,
            Phone = viewModel.Phone,
            Email = viewModel.Email
        };

        _repository.Insert(model);
    }

    public void Update(CustomerInsertUpdateViewModel viewModel)
    {
        var model = new Customer()
        {
            Id = viewModel.Id,
            CompanyName = viewModel.CompanyName,
            ContactPerson = viewModel.ContactPerson,
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

using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using BasiliskWeb.ViewModels.Delivery;

namespace BasiliskWeb.Services;

public class DeliveryService
{
    private readonly IDeliveryRepository _repository;
    public DeliveryService(IDeliveryRepository repository) {
        _repository = repository;
    }

    public DeliveryIndexViewModel Get(int pageNumber, int pageSize, string companyName) {
        var model = _repository.Get(pageNumber, pageSize, companyName)
        .Select(del => new DeliveryViewModel{
            Id = del.Id,
            CompanyName = del.CompanyName,
            Phone = del.Phone,
            Cost = del.Cost
        });

        return new DeliveryIndexViewModel() {
            Deliveries = model.ToList(),
            Pagination = new ViewModels.PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count(companyName)
            },
            CompanyName = companyName
        };
    }

    public DeliveryInsertUpdateViewModel Get(long id)
    {
        var model = _repository.Get(id);

        return new DeliveryInsertUpdateViewModel()
        {
            Id = model.Id,
            CompanyName = model.CompanyName,
            Phone = model.Phone,
            Cost = model.Cost
        };
    }

    public void Insert(DeliveryInsertUpdateViewModel viewModel)
    {
        var model = new Delivery()
        {
            CompanyName = viewModel.CompanyName,
            Phone = viewModel.Phone,
            Cost = viewModel.Cost
        };

        _repository.Insert(model);
    }

    public void Update(DeliveryInsertUpdateViewModel viewModel)
    {
        var model = new Delivery()
        {
            Id = viewModel.Id,
            CompanyName = viewModel.CompanyName,
            Phone = viewModel.Phone,
            Cost = viewModel.Cost
        };

        _repository.Update(model);
    }

    public void Delete(long id)
    {
        var model = _repository.Get(id);
        _repository.Delete(model);
    }
}

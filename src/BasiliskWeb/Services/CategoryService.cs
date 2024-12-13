using BasiliskBusiness;
using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using BasiliskWeb.ViewModels;
using BasiliskWeb.ViewModels.Category;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace BasiliskWeb.Services;

public class CategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository) {
        _repository = repository;
    }

    public CategoryIndexViewModel Get(int pageNumber, int pageSize, string name) {
        var model = _repository.Get(pageNumber, pageSize, name)
        .Select(cat => new CategoryViewModel{
            Id = cat.Id,
            Name = cat.Name,
            Description = cat.Description
        });

        return new CategoryIndexViewModel() {
            Categories = model.ToList(),
            Pagination = new PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count(name)
            },
            Name = name
        };
    }

    public CategoryInsertUpdateViewModel Get(long id)
    {
        var model = _repository.Get(id);

        return new CategoryInsertUpdateViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description
        };
    }

    public CategoryDeleteViewModel GetProduct()
    {
        throw new NullReferenceException();
    }

    public void Insert(CategoryInsertUpdateViewModel viewModel)
    {
        var model = new Category()
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Description = viewModel.Description
        };

        _repository.Insert(model);
    }

    public void Update(CategoryInsertUpdateViewModel viewModel)
    {
        var model = new Category()
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
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

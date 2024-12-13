using BasiliskAPI.Regions;
using BasiliskAPI.Salesman;
using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BasiliskAPI;

public class SalesmanService
{
    private readonly ISalesmanRepository _repository;
    private readonly IRegionRepository _regionRepository;

    public SalesmanService(ISalesmanRepository repository, IRegionRepository regionRepository)
    {
        _repository = repository;
        _regionRepository = regionRepository;
    }

    public List<SalesmanDTO> Get()
    {
        return _repository.Get().Select(salesman => new SalesmanDTO()
        {
            EmployeeNumber = salesman.EmployeeNumber,
            FullName = salesman.FirstName + " " + salesman.LastName
        }).ToList();
    }

    public SalesmanIndexDTO Get(int pageNumber, int pageSize, string employeeNumber, string fullName, string level, string superiorName)
    {
        var model = _repository.Get(pageNumber, pageSize, employeeNumber, fullName, level, superiorName)
        .Select(salesman => new SalesmanDTO()
        {
            EmployeeNumber = salesman.EmployeeNumber,
            FullName = salesman.FirstName + " " + salesman.LastName ?? "",
            Level = salesman.Level ?? "",
            Superior = salesman.SuperiorEmployeeNumberNavigation?.FirstName + " " + salesman.SuperiorEmployeeNumberNavigation?.LastName ?? ""
        });

        return new SalesmanIndexDTO()
        {
            Salesman = model.ToList(),
            Pagination = new PaginationDTO()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count(employeeNumber, fullName, level, superiorName),
            },
            EmployeeNumber = employeeNumber,
            FullName = fullName,
            Level = level,
            SuperiorName = superiorName
        };
    }

    public SalesmanGetByIdDTO Get(string employeeNumber)
    {
        var model = _repository.Get(employeeNumber);

        return new SalesmanGetByIdDTO()
        {
            EmployeeNumber = model.EmployeeNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Level = model.Level,
            BirthDate = model.BirthDate.ToString("yyyy-MM-dd"),
            HiredDate = model.HiredDate.ToString("yyyy-MM-dd"),
            City = model.City,
            Address = model.Address,
            SuperiorEmployeeNumber = model.SuperiorEmployeeNumber
        };
    }

    public void Insert(SalesmanInsertUpdateDTO dto)
    {
        var model = new BasiliskDataAccess.Models.Salesman()
        {
            EmployeeNumber = dto.EmployeeNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Level = dto.Level,
            BirthDate = dto.BirthDate,
            HiredDate = dto.HiredDate,
            Address = dto.Address,
            City = dto.City,
            SuperiorEmployeeNumber = dto.SuperiorEmployeeNumber
        };

        _repository.Insert(model);
    }

    public void Update(SalesmanInsertUpdateDTO dto)
    {
        var model = new BasiliskDataAccess.Models.Salesman()
        {
            EmployeeNumber = dto.EmployeeNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Level = dto.Level,
            BirthDate = dto.BirthDate,
            HiredDate = dto.HiredDate,
            Address = dto.Address,
            City = dto.City,
            SuperiorEmployeeNumber = dto.SuperiorEmployeeNumber
        };

        _repository.Update(model);
    }

    public void Delete(string employeeNumber)
    {
        var model = _repository.Get(employeeNumber);
        _repository.Delete(model);
    }

    public SalesmanRegionDTO GetSalesmanRegion(string employeeNumber)
    {
        var model = _repository.GetSalesmanRegion(employeeNumber);
        var salesmanRegion = new List<RegionDTO>();
        foreach (var item in model.Regions)
        {
            var region = new RegionDTO()
            {
                Id = item.Id,
                City = item.City,
                Remark = item.Remark
            };
            salesmanRegion.Add(region);
        }

        return new SalesmanRegionDTO()
        {
            EmployeeNumber = model.EmployeeNumber,
            FullName = model.FirstName + " " + model.LastName ?? "",
            SalesmanRegion = new RegionIndexDTO()
            {
                Regions = salesmanRegion
            }
        };
    }

    public void InsertSalesmanRegion(SalesmanRegionInsertDTO dto)
    {
        List<Region> regions = new List<Region>();

        foreach (var item in dto.Region)
        {
            var getRegion = _regionRepository.Get(item);
            regions.Add(getRegion);
        }

        var getSalesman = _repository.Get(dto.EmployeeNumber);
        getSalesman.Regions = regions;
        _repository.InsertSalesmanRegion(getSalesman);
    }

    public void DeleteSalesmanRegion(string employeeNumber, long regionId)
    {
        var getSalesman = _repository.GetSalesmanRegion(employeeNumber);
        var regionRemove = getSalesman.Regions.FirstOrDefault(r => r.Id == regionId);

        if (regionRemove != null)
        {
            getSalesman.Regions.Remove(regionRemove);
            _repository.DeleteSalesmanRegion(getSalesman);
        }
    }
}
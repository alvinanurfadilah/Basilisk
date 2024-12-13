using BasiliskAPI;
using BasiliskAPI.Regions;
using BasiliskAPI.Salesman;
using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
namespace BasiliskAPI.Regions;

public class RegionService
{
    private readonly IRegionRepository _repository;
    private readonly ISalesmanRepository _salesmanRepository;

    public RegionService(IRegionRepository repository, ISalesmanRepository salesmanRepository)
    {
        _repository = repository;
        _salesmanRepository = salesmanRepository;
    }

    public List<RegionDTO> Get()
    {
        return _repository.Get().Select(reg => new RegionDTO()
        {
            Id = reg.Id,
            City = reg.City
        }).ToList();
    }

    public RegionIndexDTO Get(int pageNumber, int pageSize, string city)
    {
        var model = _repository.Get(pageNumber, pageSize, city)
        .Select(reg => new RegionDTO
        {
            Id = reg.Id,
            City = reg.City ?? "",
            Remark = reg.Remark ?? ""
        });

        return new RegionIndexDTO()
        {
            Regions = model.ToList(),
            Pagination = new PaginationDTO()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count(city)
            },
            City = city
        };
    }

    public RegionSalesmanDTO GetRegionSalesman(long id, int pageNumber, int pageSize, string employeeNumber, string fullName, string level, string superiorName)
    {
        var model = _repository.GetRegionSalesman(id, employeeNumber, fullName, level, superiorName);
        var regionSalesman = new List<SalesmanDTO>();

        foreach (var item in model.SalesmanEmployeeNumbers)
        {
            var regionDTO = new SalesmanDTO()
            {
                EmployeeNumber = item.EmployeeNumber,
                FullName = item.FirstName + " " + item.LastName,
                Level = item.Level,
                Superior = item.SuperiorEmployeeNumberNavigation?.FirstName + " " + item.SuperiorEmployeeNumberNavigation?.LastName
            };
            regionSalesman.Add(regionDTO);
        }

        return new RegionSalesmanDTO()
        {
            City = model.City,
            Remark = model.Remark,
            RegionSalesman = new SalesmanIndexDTO()
            {
                Salesman = regionSalesman,
                Pagination = new PaginationDTO()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRows = _repository.Count(id, employeeNumber, fullName, level, superiorName)
                },
                RegionId = id,
                EmployeeNumber = employeeNumber,
                FullName = fullName,
                Level = level,
                SuperiorName = superiorName
            }
        };
    }

    public RegionInsertUpdateDTO Get(long id)
    {
        var model = _repository.Get(id);

        return new RegionInsertUpdateDTO()
        {
            Id = model.Id,
            City = model.City,
            Remark = model.Remark
        };
    }

    public RegionInsertUpdateDTO Insert(RegionInsertUpdateDTO dto)
    {
        var model = new Region()
        {
            City = dto.City,
            Remark = dto.Remark
        };

        var result = _repository.InsertResultId(model);
        return new RegionInsertUpdateDTO()
        {
            Id = result.Id,
            City = result.City,
            Remark = result.Remark
        };
    }

    public RegionInsertUpdateDTO Update(RegionInsertUpdateDTO dto)
    {
        var model = new Region()
        {
            Id = dto.Id,
            City = dto.City,
            Remark = dto.Remark
        };

        var result = _repository.UpdateResultId(model);
        return new RegionInsertUpdateDTO()
        {
            Id = result.Id,
            City = result.City,
            Remark = result.Remark
        };
    }

    public void Delete(long id)
    {
        var model = _repository.Get(id);
        _repository.Delete(model);
    }

    public void InsertRegionSalesman(RegionSalesmanInsertDTO dto)
    {
        List<BasiliskDataAccess.Models.Salesman> salesmen = new List<BasiliskDataAccess.Models.Salesman>();

        foreach (var item in dto.Salesman)
        {
            var getSalesman = _salesmanRepository.Get(item);
            salesmen.Add(getSalesman);
        }

        var getRegion = _repository.Get(dto.RegionId);
        getRegion.SalesmanEmployeeNumbers = salesmen;
        _repository.InsertRegionSalesman(getRegion);
    }

    public void DeleteRegionSalesman(long id, string employeeNumber)
    {
        var getRegion = _repository.GetRegionSalesman(id);
        var salesmanRemove = getRegion.SalesmanEmployeeNumbers.FirstOrDefault(s => s.EmployeeNumber == employeeNumber);

        if (salesmanRemove != null)
        {
            getRegion.SalesmanEmployeeNumbers.Remove(salesmanRemove);
            _repository.DeleteRegionSalesman(getRegion);
        }
    }
}
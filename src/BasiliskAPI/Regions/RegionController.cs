using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskAPI.Regions;

[Route("api/v1/region")]
[ApiController]
[Authorize]
public class RegionController : ControllerBase
{
    private readonly RegionService _service;

    public RegionController(RegionService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public IActionResult Get()
    {
        var dto = _service.Get();
        return Ok(dto);
    }

    [HttpGet]
    public IActionResult Get(int pageNumber = 1, int pageSize = 10, string? city = "")
    {
        var dto = _service.Get(pageNumber, pageSize, city);
        return Ok(dto);
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        var dto = _service.Get(id);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Insert(RegionInsertUpdateDTO dto)
    {
        dto = _service.Insert(dto);
        return Created("", dto);
    }

    [HttpPut]
    public IActionResult Put(RegionInsertUpdateDTO dto)
    {
        dto = _service.Update(dto);
        return Ok(dto);
    }

    [HttpDelete]
    public IActionResult Delete(long id)
    {
        _service.Delete(id);
        return Ok($"Region dengan id {id} berhasil dihapus");
    }

    [HttpGet("regionsalesman/{id}")]
    public IActionResult GetRegionSalesman(long id, string? employeeNumber, string? fullName, string? level, string? superiorName, int pageNumber = 1, int pageSize = 10)
    {
        employeeNumber = "";
        fullName = "";
        level = "";
        superiorName = "";
        var dto = _service.GetRegionSalesman(id, pageNumber, pageSize, employeeNumber, fullName, level, superiorName);
        return Ok(dto);
    }

    [HttpPost("regionsalesman")]
    public IActionResult InsertRegionSalesman(RegionSalesmanInsertDTO dto)
    {
        _service.InsertRegionSalesman(dto);
        return Ok();
    }

    [HttpDelete("regionsalesman")]
    public IActionResult DeleteRegionSalesman(long id, string employeeNumber)
    {
        _service.DeleteRegionSalesman(id, employeeNumber);
        return Ok();
    }
}
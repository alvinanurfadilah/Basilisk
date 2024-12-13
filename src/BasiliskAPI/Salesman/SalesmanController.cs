using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskAPI.Salesman;

[Route("api/v1/salesman")]
[ApiController]
[Authorize]
public class SalesmanController : ControllerBase
{
    private readonly SalesmanService _service;

    public SalesmanController(SalesmanService service)
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
    public IActionResult Get(int pageNumber = 1, int pageSize = 10, string? employeeNumber = "", string? fullName = "", string? level = "", string? superiorName = "")
    {
        var dto = _service.Get(pageNumber, pageSize, employeeNumber, fullName, level, superiorName);
        return Ok(dto);
    }

    [HttpGet("{employeeNumber}")]
    public IActionResult Get(string employeeNumber)
    {
        var dto = _service.Get(employeeNumber);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Insert(SalesmanInsertUpdateDTO dto)
    {
        _service.Insert(dto);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update(SalesmanInsertUpdateDTO dto)
    {
        _service.Update(dto);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete(string employeeNumber)
    {
        _service.Delete(employeeNumber);
        return Ok($"Salesman dengan employee number {employeeNumber} berhasil dihapus");
    }


    [HttpGet("salesmanregion/{employeeNumber}")]
    public IActionResult GetSalesmanRegion(string employeeNumber)
    {
        var dto = _service.GetSalesmanRegion(employeeNumber);
        return Ok(dto);
    }

    [HttpPost("salesmanregion")]
    public IActionResult InsertSalesmanRegion(SalesmanRegionInsertDTO dto)
    {
        _service.InsertSalesmanRegion(dto);
        return Ok();
    }

    [HttpDelete("salesmanregion")]
    public IActionResult DeleteSalesmanRegion(string employeeNumber, long regionId)
    {
        _service.DeleteSalesmanRegion(employeeNumber, regionId);
        return Ok();
    }
}

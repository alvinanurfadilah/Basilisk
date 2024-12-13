using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BasiliskWeb.BackendGateway;

[Route("api/v1/salesman")]
[ApiController]
[Authorize]
public class SalesmanController : ControllerBase
{

    private readonly SalesmanGatewayService _service;

    public SalesmanController(SalesmanGatewayService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public async Task<IActionResult> Get()
    {
        var token = User.FindFirst("Token")?.Value ?? string.Empty;
        var response = await _service.Get(token);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? employeeNumber = "", string? fullName = "", string? level = "", string? superiorName = "", int pageNumber = 1, int pageSize = 10)
    {
        var token = User.FindFirst("Token")?.Value;
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Token is missing or invalid.");
        }
        var response = await _service.Get(pageNumber, pageSize, employeeNumber, fullName, level, superiorName, token);

        return Ok(response);
    }

    [HttpGet("{employeeNumber}")]
    public async Task<IActionResult> Get(string employeeNumber)
    {
        var token = User.FindFirst("Token")?.Value ?? string.Empty;
        var response = await _service.Get(employeeNumber, token);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SalesmanInsertUpdateDTO dto)
    {
        var token = User.FindFirst("Token")?.Value ?? string.Empty;
        var response = await _service.Insert(dto, token);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(SalesmanInsertUpdateDTO dto)
    {
        var token = User.FindFirst("Token")?.Value ?? string.Empty;
        var response = await _service.Update(dto, token);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string employeeNumber)
    {
        var token = User.FindFirst("Token")?.Value ?? string.Empty;
        var response = await _service.Delete(employeeNumber, token);
        return Ok(response);
    }

    [HttpGet("allRegion")]
    public async Task<IActionResult> GetRegionDropdown()
    {
        var token = User.FindFirst("Token")?.Value ?? string.Empty;
        var response = await _service.GetRegionDropdown(token);
        return Ok(response);
    }

    [HttpGet("salesmanregion/{employeeNumber}")]
    public async Task<IActionResult> GetSalesmanRegion(string employeeNumber)
    {
        var token = User.FindFirst("Token")?.Value ?? string.Empty;
        var response = await _service.GetSalesmanRegion(employeeNumber, token);
        return Ok(response);
    }

    [HttpPost("salesmanregion")]
    public async Task<IActionResult> InsertSalesmanRegion(SalesmanRegionInsertDTO dto)
    {
        var token = User.FindFirst("Token")?.Value ?? string.Empty;
        var response = await _service.InsertSalesmanRegion(dto, token);
        return Ok(response);
    }

    [HttpDelete("salesmanregion")]
    public async Task<IActionResult> DeleteSalesmanRegion(string employeeNumber, long regionId)
    {
        var token = User.FindFirst("Token")?.Value ?? string.Empty;
        var response = await _service.DeleteSalesmanRegion(employeeNumber, regionId, token);
        return Ok(response);
    }
}
using Microsoft.AspNetCore.Mvc;

namespace BasiliskAPI.Accounts;

[Route("api/v1/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;
    public AccountController(AccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Login(AccountRequestDTO request)
    {
        try
        {
            var response = _service.GetToken(request);
            return Ok(response);
        }
        catch (System.Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

using System.Security.Claims;
using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var viewModel = _service.GetLogin();
        return RedirectToAction("Login", viewModel);
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        if (User?.Identity?.IsAuthenticated ?? true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        var viewModel = _service.GetLogin();
        return View(viewModel);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(AccountLoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var ticket = await _service.SetLogin(viewModel);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ticket.Principal,
                    ticket.Properties
                );
                return RedirectToAction("Index", "Dashboard");
            }
            catch (System.Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
        }
        var vm = _service.GetLogin();
        return View(vm);
    }

    [HttpGet("Register")]
    public IActionResult Register()
    {
        if (!User?.Identity?.IsAuthenticated ?? true)
        {
            var viewModel = _service.GetRegister();
            return View(viewModel);
        }
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpPost("Register")]
    public IActionResult Register(AccountRegisterViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.AddRegister(viewModel);
            return RedirectToAction("Login");
        }
        var vm = _service.GetRegister();
        return View(vm);
    }

    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    [Authorize]
    [HttpGet("ChangePassword")]
    public IActionResult ChangePassword()
    {
        return View("ChangePassword");
    }

    [Authorize]
    [HttpPost("ChangePassword")]
    public IActionResult ChangePassword(AccountChangePasswordViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var getUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
                viewModel.Username = getUsername;
                _service.ChangePassword(viewModel);
                return RedirectToAction("Index", "Dashboard");
            }
            catch (System.Exception e)
            {
                ViewBag.Message = e.Message;
            }
        }
        return View("ChangePassword");
    }
}
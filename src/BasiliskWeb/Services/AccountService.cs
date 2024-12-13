using System.Security.Claims;
using BasiliskBusiness.Exceptions;
using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using BasiliskWeb.BackendGateway;
using BasiliskWeb.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.Services;

public class AccountService
{
    private readonly IAccountRepository _repository;
    private readonly IConfiguration _configuration;

    public AccountService(IAccountRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    private async Task<string> GetToken(AccountLoginViewModel viewModel)
    {
        var request = new AuthRequestDTO()
        {
            Username = viewModel.Username,
            Password = viewModel.Password,
            Role = viewModel.Role
        };

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:8080");
        var response = await client.PostAsJsonAsync("/api/v1/account", request);
        AuthResponseDTO content = new AuthResponseDTO();
        content.Token = await response.Content.ReadAsStringAsync();
        return content.Token;
    }

    private async Task<ClaimsPrincipal> GetPrincipal(AccountLoginViewModel viewModel)
    {
        var token = await GetToken(viewModel);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, viewModel.Username),
            new Claim(ClaimTypes.Role, viewModel.Role??string.Empty),
            new Claim("Token", token)
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

    private AuthenticationTicket GetTicket(ClaimsPrincipal principal)
    {
        AuthenticationProperties authenticationProperties = new AuthenticationProperties()
        {
            IssuedUtc = DateTime.Now,
            ExpiresUtc = DateTime.Now.AddMinutes(30),
            AllowRefresh = false
        };

        AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme);

        return authenticationTicket;
    }

    public async Task<AuthenticationTicket> SetLogin(AccountLoginViewModel viewModel)
    {
        var model = _repository.Get(viewModel.Username);
        bool isUsername = viewModel.Username == model.Username;
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(viewModel.Password, model.Password);
        bool isRole = viewModel.Role == model.Role;
        if (isUsername && isCorrectPassword && isRole)
        {
            Task<ClaimsPrincipal> principal = GetPrincipal(viewModel);
            AuthenticationTicket ticket = GetTicket(await principal);

            return ticket;
        } 
        else 
        {
            throw new UsernamePasswordException("Username or Password or Role is invalid");
        }
    }

    private List<SelectListItem> GetRoles()
    {
        return new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Text = "Admin",
                Value = "Admin"
            },
            new SelectListItem()
            {
                Text = "Seller",
                Value = "Seller"
            }
        };
    }

    public AccountLoginViewModel GetLogin()
    {
        return new AccountLoginViewModel()
        {
            Roles = GetRoles()
        };
    }

    public AccountRegisterViewModel GetRegister()
    {
        return new AccountRegisterViewModel()
        {
            Roles = GetRoles()
        };
    }

    public void AddRegister(AccountRegisterViewModel viewModel)
    {
        var model = new Account()
        {
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            Role = viewModel.Role
        };
        _repository.Insert(model);
    }

    public void ChangePassword(AccountChangePasswordViewModel viewModel)
    {
        var model = _repository.Get(viewModel.Username);
        var isOldPassword = BCrypt.Net.BCrypt.Verify(viewModel.OldPassword, model.Password);
        if (!isOldPassword)
        {
            throw new Exception("Password is incorrect!");
        }
        model.Password = BCrypt.Net.BCrypt.HashPassword(viewModel.NewPassword);
        _repository.Update(model);
    }
}

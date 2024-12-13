using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BasiliskBusiness.Exceptions;
using BasiliskBusiness.Interface;
using BasiliskDataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace BasiliskAPI.Accounts;

public class AccountService
{
    private readonly IAccountRepository _repository;
    private readonly IConfiguration _configuration;

    public AccountService(IAccountRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    private string CreateToken(Account model)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, model.Username),
            new Claim(ClaimTypes.Role, model.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value
            )
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return serializedToken;
    }

    public string GetToken(AccountRequestDTO request)
    {   
        var model = _repository.Get(request.Username);
        bool isUsername = request.Username == model.Username;
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(request.Password, model.Password);
        bool isRole = request.Role == model.Role;
        
        if (isUsername && isCorrectPassword && isRole)
        {
            return CreateToken(model);
        } else 
        {
            throw new UsernamePasswordException("Username or Password or Role is incorrect");
        }
    }
}
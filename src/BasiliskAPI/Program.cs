using System.Text;
using BasiliskAPI.Accounts;
using BasiliskAPI.Regions;
using BasiliskBusiness;
using BasiliskBusiness.Interface;
using BasiliskBusiness.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using static BasiliskDataAccess.Dependencies;

namespace BasiliskAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IConfiguration configuration = builder.Configuration;
        IServiceCollection services = builder.Services;
        ConfigureService(configuration, services);
        services.AddControllers();

        // services.AddSwaggerGen();

        services.AddScoped<IRegionRepository, RegionRepository>();
        services.AddScoped<RegionService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<AccountService>();
        services.AddScoped<ISalesmanRepository, SalesmanRepository>();
        services.AddScoped<SalesmanService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowFrontEnd", builder =>
            {
                builder.WithOrigins("http://localhost:5196")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)
                ),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        // services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Basilisk Api"
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Description = "Example value = Bearer: token",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        var app = builder.Build();

        app.UseCors("AllowFrontEnd");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
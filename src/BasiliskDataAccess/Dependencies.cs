using BasiliskDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasiliskDataAccess;

public static class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection services) {
        services.AddDbContext<BasiliskTFContext>(
            option => option.UseSqlServer(configuration.GetConnectionString("BasiliskTFConnection"))
        );
    }
}
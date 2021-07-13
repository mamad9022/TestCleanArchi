using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Persistence.Context;

namespace TestCleanArch.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestCleanArchDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NorthwindDatabase")),
                ServiceLifetime.Transient);

            services.AddScoped<ITestCleanArchDbContext>(provider => provider.GetService<TestCleanArchDbContext>());

            return services;
        }
    }
}

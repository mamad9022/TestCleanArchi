using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Application.Common.Service;

namespace TestCleanArch.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IUserService, UserService>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
            services.AddMemoryCache(); // Add this line


            return services;
        }
    }
}

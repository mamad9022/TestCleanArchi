using System;
using System.Reflection;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Application.Common.RabbitMq;
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
            services.AddTransient<IUserService, UserService>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
            services.AddMemoryCache(); // Add this line

            #region Rabbit

            services.TryAddSingleton<IRabbitMqConnection>
                (sp => sp.GetRequiredService<IOptions<RabbitMqConnection>>().Value);

            services.AddScoped<IBusPublish, BusPublish>();

            #endregion

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using TestCleanArch.Common.Interface;
using TestCleanArch.Common.Services;

namespace TestCleanArch.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            services.AddTransient<IMessageSendService, SmsService>();

            return services;

        }
    }
}

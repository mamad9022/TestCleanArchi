using System;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TestCleanArch.Api.Middleware;
using TestCleanArch.Application;
using TestCleanArch.Application.Authorize;
using TestCleanArch.Application.Common.RabbitMq;
using TestCleanArch.Common;
using TestCleanArch.Persistence;

namespace TestCleanArch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddCors();

            services.AddApplication();

            services.AddPersistence(Configuration);

            services.AddCommon();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestCleanArch.Api", Version = "v1" });
            });

            #region Hangfire
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
            #endregion

            #region RabbitMq
            services.Configure<RabbitMqConnection>(Configuration.GetSection("RabbitMqConnection"));
            #endregion

            #region Logger
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<ExceptionMiddleware>>();
            services.AddSingleton(typeof(ILogger), logger);
            #endregion 

          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestCleanArch.Api v1"));
            }
            else
            {
                app.UseHsts();
            }
            app.UseHangfireDashboard();

            app.UseRouting();

            app.UseCors(x => x
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials()); // allow credentials

            //  app.UseAuthorization();
            #region middleware
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<UserIpMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}

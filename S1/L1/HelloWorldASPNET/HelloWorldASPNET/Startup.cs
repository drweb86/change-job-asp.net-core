using System;
using HelloWorldASPNET.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HelloWorldASPNET
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment appEnv)
        {
            // dotnet ef migrations add InitialCreate
            Configuration = new ConfigurationBuilder()
                .SetBasePath(appEnv.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IPuppyService, DogCare>();
            services.AddMvc();
            services.AddEntityFramework()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<LBartoContext>(
                    options => options.UseSqlServer(Configuration["connectionStrings:" + Configuration["environment"]]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IPuppyService puppyService)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseMvc(ConfigureRoutes);

            app.Run(async (context) => // Terminal piece of middleware
            {
                // context.Request
                // var greeting = Configuration["greeting"];
                var greeting = puppyService.DoSound();
                await context.Response.WriteAsync(greeting);
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}

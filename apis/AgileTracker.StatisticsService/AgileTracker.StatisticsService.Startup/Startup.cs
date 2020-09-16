namespace AgileTracker.StatisticsService.Startup
{
    using System.Collections.Generic;

    using AgileTracker.StatisticsService.Application;
    using AgileTracker.StatisticsService.Domain;
    using AgileTracker.StatisticsService.Infrastructure;
    using AgileTracker.StatisticsService.Web;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Newtonsoft.Json;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDomain()
                .AddApplication(this.Configuration)
                .AddInfrastructure(this.Configuration)
                .AddRabbit()
                .AddWebComponents()
                .AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Initialize();

            app.UseSwaggerWithUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

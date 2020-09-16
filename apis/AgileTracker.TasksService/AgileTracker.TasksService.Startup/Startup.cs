namespace AgileTracker.TasksService.Startup
{
    using AgileTracker.Common.Web;
    using AgileTracker.TasksService.Application;
    using AgileTracker.TasksService.Domain;
    using AgileTracker.TasksService.Infrastructure;
    using AgileTracker.TasksService.Web;
    using AgileTracker.TasksService.Web.Middleware;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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

            services.AddCors();
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
            app.UseValidationExceptionHandler();

            app.UseMiddleware<TestMiddleware>();

            app.UseCors(cors => cors
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

namespace AgileTracker.StatisticsService.Web
{
    using System;
    using System.IO;
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    using Swashbuckle.AspNetCore.Filters;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
             services
                .AddControllers()
                .AddNewtonsoftJson();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgileTracker Statistics Internal API", Version = "v1" });

                    c.EnableAnnotations();
                    c.ExampleFilters();

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });

            services.AddSwaggerExamples();
            services.AddSwaggerExamplesFromAssemblyOf(typeof(WebConfiguration));

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
        {
            return app
                    .UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgileTracker Statistics Internal API");
                        c.DefaultModelsExpandDepth(-1);
                    });
        }
    }
}

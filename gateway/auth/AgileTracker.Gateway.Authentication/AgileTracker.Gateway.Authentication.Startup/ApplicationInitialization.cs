using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AgileTracker.Common.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AgileTracker.Gateway.Authentication.Startup
{
    public static class ApplicationInitialization
    {
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IInitializer>();

            foreach(var initializer in initializers)
            {
                initializer.Initialize();
            }

            return app;
        }
    }
}

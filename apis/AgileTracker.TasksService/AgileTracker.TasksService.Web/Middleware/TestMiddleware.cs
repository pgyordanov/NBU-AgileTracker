namespace AgileTracker.TasksService.Web.Middleware
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class TestMiddleware
    {
        private RequestDelegate next;

        public TestMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

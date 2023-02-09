using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Prommerce.Application.Resources.Users;

namespace Prommerce.Application
{
    public static class ApplicationEnpoints
    {
        public static WebApplication MapApplicationEndpoints(this WebApplication app)
        {
            app.MapGroup("/users")
                .MapUserEndpoints()
                .WithTags("Users Endpoints");

            return app;
        }
    }
}
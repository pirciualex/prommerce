using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Prommerce.Common.Extensions;
using System.Reflection;

namespace Prommerce.Application.RouteHandlers
{
    public static class ApplicationEnpoints
    {
        public static WebApplication MapApplicationEndpoints(this WebApplication app)
        {
            var endpointsGroupInterfaceType = typeof(IEndpointsGroup);
            var endpointsGroupTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType &&
                t.GetConstructor(Type.EmptyTypes) != null &&
                endpointsGroupInterfaceType.IsAssignableFrom(t));
            foreach (var endpointsGroupType in endpointsGroupTypes)
            {
                var instantiatedEndpointsGroup = (IEndpointsGroup)Activator
                    .CreateInstance(endpointsGroupType)!;
                string groupName = endpointsGroupType.Name.Humanize().Split(" ")[0];
                var endpointsGroup = app.MapGroup($"/api/{groupName.FirstCharToLower()}");
                instantiatedEndpointsGroup
                    .MapEndpoints(endpointsGroup)
                    .WithTags($"{groupName} Endpoints");
            }

            return app;
        }
    }
}
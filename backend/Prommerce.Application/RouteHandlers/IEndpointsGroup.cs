using Microsoft.AspNetCore.Routing;

namespace Prommerce.Application.RouteHandlers
{
    public interface IEndpointsGroup
    {
        RouteGroupBuilder MapEndpoints(RouteGroupBuilder group);
    }
}
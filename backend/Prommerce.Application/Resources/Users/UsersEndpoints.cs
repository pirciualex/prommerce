using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Prommerce.Application.Resources.Users.Models;
using Prommerce.Application.Resources.Users.Requests;
using Prommerce.Application.RouteHandlers;

namespace Prommerce.Application.Resources.Users
{
    public class UsersEndpoints : IEndpointsGroup
    {
        public RouteGroupBuilder MapEndpoints(RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll.Handle)
                //.AddEndpointFilterFactory(RequestAuditor)
                .AllowAnonymous();
            group.MapGet("/{id}", Get.Handle);
            group
                .MapPost("/", Add.Handle)
                .AddEndpointFilter(async (efiContext, next) =>
                {
                    var param = efiContext.GetArgument<UserPostDto>(0);

                    var validationErrors = Validation.IsValid(param);

                    if (validationErrors.Any())
                    {
                        return Results.ValidationProblem(validationErrors);
                    }

                    return await next(efiContext);
                });

            group.MapPut("/{id}", Update.Handle);
            group.MapDelete("/{id}", Delete.Handle);

            return group;
        }
    }
}
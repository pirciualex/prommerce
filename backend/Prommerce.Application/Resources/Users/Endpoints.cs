using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Prommerce.Application.Resources.Users.Models;
using Prommerce.Data;
using Prommerce.Data.Entites;

namespace Prommerce.Application.Resources.Users
{
    public static class Endpoints
    {
        public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllUsers)
                .AllowAnonymous();
            group.MapGet("/{id}", GetUser);
            group
                .MapPost("/", AddUser)
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

            group.MapPut("/{id}", UpdateUser);
            group.MapDelete("/{id}", DeleteUser);

            return group;
        }

        internal static async Task<IResult> GetAllUsers(Db db, [FromServices] IMapper mapper)
        {
            var users = await db.Users
                .Take(1000)
                .Select(u => mapper.Map<UserGetDto>(u))
                .ToListAsync();
            return TypedResults.Ok(users);
        }

        internal static async Task<IResult> GetUser(Guid id, Db db, [FromServices] IMapper mapper)
        {
            var user = await db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == default)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(mapper.Map<UserGetDto>(user));
        }

        internal static async Task<IResult> AddUser(UserPostDto user, Db db, [FromServices] IMapper mapper)
        {
            var userToAdd = mapper.Map<User>(user);
            await db.Users.AddAsync(userToAdd);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/users/{userToAdd.Id}", user);
        }

        internal static async Task<IResult> UpdateUser(Guid id, UserPutDto user, [FromServices] IValidator<UserPutDto> validator, Db db, [FromServices] IMapper mapper)
        {
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(err => new { errors = err.ErrorMessage });
                return TypedResults.BadRequest(errors);
            }

            var userToUpdate = await db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (userToUpdate == default)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(user, userToUpdate);

            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        internal static async Task<IResult> DeleteUser(Guid id, Db db)
        {
            var userToUpdate = await db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (userToUpdate == default)
            {
                return TypedResults.NotFound();
            }

            db.Users.Remove(userToUpdate);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prommerce.Application.Resources.Users.Models;
using Prommerce.Data;
using Prommerce.Data.Entites;

namespace Prommerce.Application.Resources.Users.Requests
{
    internal class Add
    {
        internal static async Task<IResult> Handle(UserPostDto user, Db db, [FromServices] IMapper mapper)
        {
            var userToAdd = mapper.Map<User>(user);
            await db.Users.AddAsync(userToAdd);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/users/{userToAdd.Id}", user);
        }
    }
}
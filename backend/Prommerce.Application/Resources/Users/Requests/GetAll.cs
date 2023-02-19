using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prommerce.Application.Resources.Users.Models;
using Prommerce.Data;

namespace Prommerce.Application.Resources.Users.Requests
{
    internal static class GetAll
    {
        internal static async Task<IResult> Handle(Db db, [FromServices] IMapper mapper)
        {
            var users = await db.Users
                .Take(1000)
                .Select(u => mapper.Map<UserGetDto>(u))
                .ToListAsync();
            return TypedResults.Ok(users);
        }
    }
}
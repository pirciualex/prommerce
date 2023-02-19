using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prommerce.Application.Resources.Users.Models;
using Prommerce.Data;

namespace Prommerce.Application.Resources.Users.Requests
{
    internal class Get
    {
        internal static async Task<IResult> Handle(Guid id, Db db, [FromServices] IMapper mapper)
        {
            var user = await db.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == default)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(mapper.Map<UserGetDto>(user));
        }
    }
}
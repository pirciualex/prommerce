using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prommerce.Application.Resources.Users.Models;
using Prommerce.Data;

namespace Prommerce.Application.Resources.Users.Requests
{
    internal class Update
    {
        internal static async Task<IResult> Handle(Guid id, UserPutDto user, [FromServices] IValidator<UserPutDto> validator, Db db, [FromServices] IMapper mapper)
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
    }
}
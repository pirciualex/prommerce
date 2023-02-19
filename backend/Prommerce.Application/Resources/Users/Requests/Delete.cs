using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prommerce.Data;

namespace Prommerce.Application.Resources.Users.Requests
{
    internal class Delete
    {
        internal static async Task<IResult> Handle(Guid id, Db db)
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
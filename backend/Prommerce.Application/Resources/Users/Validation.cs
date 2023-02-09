using Prommerce.Application.Resources.Users.Models;

namespace Prommerce.Application.Resources.Users
{
    public static class Validation
    {
        internal static Dictionary<string, string[]> IsValid(UserPostDto user)
        {
            Dictionary<string, string[]> errors = new();

            if (string.IsNullOrEmpty(user.FirstName))
            {
                errors.TryAdd("user.firstName.errors", new[] { "First name should not be empty" });
            }

            return errors;
        }
    }
}
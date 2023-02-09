using FluentValidation;
using Prommerce.Application.Resources.Users.Models;

namespace Prommerce.Application.Resources.Users
{
    internal class UserValidator : AbstractValidator<UserPutDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}
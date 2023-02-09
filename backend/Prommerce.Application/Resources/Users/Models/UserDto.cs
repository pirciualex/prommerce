using static Prommerce.Common.Constants;

namespace Prommerce.Application.Resources.Users.Models
{
    public class UserDto
    {
        public string? Identifier { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Genders Gender { get; set; }
        public DateTimeOffset BirthdateDate { get; set; }
        public string? Email { get; set; }
        public string? ProfilePicture { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
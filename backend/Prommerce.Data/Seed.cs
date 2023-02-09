using Bogus;
using Bogus.Extensions.UnitedStates;
using Microsoft.EntityFrameworkCore;
using Prommerce.Data.Entites;
using static Bogus.DataSets.Name;
using static Prommerce.Common.Constants;

namespace Prommerce.Data
{
    public static class Seed
    {
        private static Faker<User> _fakeUsers =
            new Faker<User>()
            .RuleFor(u => u.Identifier, f => f.Person.Ssn())
            .RuleFor(u => u.Gender, f => f.PickRandom<Genders>())
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender.ConvertAppGendersToBogus()))
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender.ConvertAppGendersToBogus()))
            .RuleFor(u => u.BirthdateDate, f => f.Person.DateOfBirth)
            .RuleFor(u => u.ProfilePicture, f => f.Person.Avatar)
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.CreatedDate, f => f.Date.Past(2))
            .RuleFor(u => u.ModifiedDate, f => f.Date.Past(2));

        private static List<User> _dbUsers = new List<User>();

        public static async Task SeedData(Db db)
        {
            db.Database.EnsureCreated();
            if (await db.Users.AnyAsync())
            {
                return;
            }

            await SeedUsers(db);
            await db.SaveChangesAsync();
        }

        private static async Task SeedUsers(Db db)
        {
            var testUser =
                new User
                {
                    Id = Guid.Parse("de0297d9-33d9-4abb-b9d2-9eb36dccb01f"),
                    Identifier = "1",
                    FirstName = "Test",
                    LastName = "User",
                    BirthdateDate = DateTimeOffset.MinValue,
                    ProfilePicture = "https://img.freepik.com/free-vector/businessman-character-avatar-isolated_24877-60111.jpg?w=740&t=st=1665314823~exp=1665315423~hmac=b116a0a675157dbc826abe58c0bb722f344672e86bb63fcbdd3f014be2ecab69",
                    Email = "test.user@solf.ro",
                    PhoneNumber = "1",
                    CreatedDate = DateTimeOffset.Now,
                    CreatedBy = "de0297d9-33d9-4abb-b9d2-9eb36dccb01f",
                    ModifiedDate = DateTimeOffset.Now,
                    ModifiedBy = "de0297d9-33d9-4abb-b9d2-9eb36dccb01f"
                };
            var users = _fakeUsers.Generate(99999).ToList();
            _dbUsers.AddRange(users);
            _dbUsers.Add(testUser);
            await db.Users.AddRangeAsync(_dbUsers);
        }

        private static Gender ConvertAppGendersToBogus(this Genders appGender)
        {
            switch (appGender)
            {
                case Genders.Male:
                    return Gender.Male;

                case Genders.Female:
                    return Gender.Female;

                case Genders.Binary:
                case Genders.Other:
                case Genders.NotSpecified:
                default:
                    Array values = Enum.GetValues(typeof(Gender));
                    Random random = new Random();
                    Gender randomBar = (Gender)values.GetValue(random.Next(values.Length));
                    return randomBar;
            }
        }
    }
}
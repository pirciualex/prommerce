using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Prommerce.Data;

namespace Prommerce.API.Initializer
{
    public class DbInitializer : IInitializer
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Db _db;

        public DbInitializer(IWebHostEnvironment webHostEnvironment, Db db)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        public int Priority => 1;

        public async Task Initialise()
        {
            if (_webHostEnvironment.IsDevelopment())
            {
                await Seed.SeedData(_db);
            }
            else
            {
                await _db.Database.MigrateAsync();
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WineAPI.Model
{
    public class WineDbContext : DbContext
    {
        public DbSet<Grape> Grapes { get; set; }
        public string? _codeFirstUpdateConnectionString;

        public string CodeFirstUpdateConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_codeFirstUpdateConnectionString))
                {
                    var config = new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
                        .Build();
                    _codeFirstUpdateConnectionString = config.GetSection("ConnectionStrings")["WineDbConnection"];
                }
                return _codeFirstUpdateConnectionString;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CodeFirstUpdateConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
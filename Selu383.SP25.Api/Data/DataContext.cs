using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Selu383.SP25.Api.Entities;

namespace Selu383.SP25.Api.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Theater> Theaters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Theater>().HasData(
                new Theater { Id = 1, Name = "Lions Den Grande Theater", Address = "123 W University Ave", SeatCount = 200 },
                new Theater { Id = 2, Name = "Lions Den Mega Theater", Address = "456 Palace Dr", SeatCount = 150 },
                new Theater { Id = 3, Name = "Lions Den Green and Gold Theater", Address = "789 S Range Rd", SeatCount = 100 }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("TheaterDB"));
        }
    }
}

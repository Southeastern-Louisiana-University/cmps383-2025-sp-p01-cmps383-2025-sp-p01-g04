using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Selu383.SP25.Api.Entities;

namespace Selu383.SP25.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
            
        {
            
        }

        public DbSet<Theater> Theaters { get; set; }

      
    }
}

using Selu383.SP25.Api.Data;
using Selu383.SP25.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Selu383.SP25.Api.Seeding
{
    public class DbInitializer
    {
        internal static void Initialize(DataContext dbContext)
        {
            dbContext.Database.Migrate();

            if (!dbContext.Theaters.Any())
            {
                var theaters = new Theater[]
                {
                    new Theater { Name = "Lions Den Grande Theater", Address = "123 W University Ave", SeatCount = 26 },
                    new Theater { Name = "Lions Den Mega Theater", Address = "456 Palace Dr", SeatCount = 30 },
                    new Theater { Name = "Lions Den Green and Gold Theater", Address = "789 S Range Rd", SeatCount = 28 }
                };

                dbContext.Theaters.AddRange(theaters);
                dbContext.SaveChanges();
            }
        }
    }
}

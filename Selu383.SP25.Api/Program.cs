using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Data;
using Selu383.SP25.Api.Seeding;
using Microsoft.OpenApi.Models;

namespace Selu383.SP25.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMPS 383 API", Version = "v1" });
            });


            var connectionString = builder.Configuration.GetConnectionString("DataContext");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<DbInitializer>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                DbInitializer.Initialize(dbContext);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMPS 383 API v1"));
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

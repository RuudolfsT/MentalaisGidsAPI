using System.Configuration;
using MentalaisGidsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MentalaisGidsAPI
{
    public class Program
    {
        static readonly string DbConnectionStringName = "MentalaisGids";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? sus = builder.Configuration.GetConnectionString(DbConnectionStringName);

            if (string.IsNullOrEmpty(sus))
            {
                throw new ConfigurationErrorsException(
                    $"No Connection String '{DbConnectionStringName}' found!"
                );
            }

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MentalaisGidsContext>(options =>
            {
                options.UseSqlServer(sus);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

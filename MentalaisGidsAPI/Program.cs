using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer;
using ServiceLayer.Interface;
using System.Configuration;
using System.Text;

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

            builder
                .Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MentalaisGidsContext>(options =>
            {
                options.UseSqlServer(sus);
            });


            builder.Services.AddScoped<ILomaManager, LomaManager>();
            builder.Services.AddScoped<IRakstsManager, RakstsManager>();
            builder.Services.AddScoped<IJwt, Jwt>();
            builder.Services.AddScoped<ILietotajsManager, LietotajsManager>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

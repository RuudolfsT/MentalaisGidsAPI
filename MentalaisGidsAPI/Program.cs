﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer;
using ServiceLayer.Interface;
using ServiceLayer.Manager;
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
                        ValidateIssuer = false, // neliekam uz īstām mājaslapām tā kā var iztikt bez issuer, audience pārbaudes
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admins", policy => policy.RequireRole("Admins"));
                options.AddPolicy("Speciālists", policy => policy.RequireRole("Speciālists"));
                options.AddPolicy("Parastais lietotājs", policy => policy.RequireRole("Parastais lietotājs"));
            });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MentalaisGidsContext>(options =>
            {
                options.UseSqlServer(sus);
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILomaManager, LomaManager>();
            builder.Services.AddScoped<IRakstsManager, RakstsManager>();
            builder.Services.AddScoped<ISazinaManager, SazinaManager>();
            builder.Services.AddScoped<ILietotajsRakstsVertejumsManager, LietotajsRakstsVertejumsManager>();
            builder.Services.AddScoped<ILietotajsRakstsKomentarsManager, LietotajsRakstsKomentarsManager>();
            builder.Services.AddScoped<ISajutuNovertejumsManager, SajutuNovertejumsManager>();
            builder.Services.AddScoped<INodarbibaManager, NodarbibaManager>();
            builder.Services.AddScoped<IJwt, Jwt>();
            builder.Services.AddScoped<ILietotajsManager, LietotajsManager>();
            builder.Services.AddScoped(typeof(IBaseManager<>), typeof(BaseManager<>));
            builder.Services.AddScoped<ILietotajsLomaManager, LietotajsLomaManager>();
            builder.Services.AddScoped<ILietotajsSpecialistsVertejumsManager, LietotajsSpecialistsVertejumsManager>();

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

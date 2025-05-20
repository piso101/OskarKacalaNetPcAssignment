using Microsoft.EntityFrameworkCore;
using BackEnd.Infrastructure.Data;
using BackEnd.Domain.Interfaces;
using BackEnd.Infrastructure.Repositories;
using BackEnd.Application.Services;
using BackEnd.Shared.Helpers;
using BackEnd.Infrastructure.Configurations;
using Microsoft.OpenApi.Models;
using BackEnd.Shared.Mappings;

namespace BackEnd.API;

/// <summary>
/// Główna klasa uruchomieniowa aplikacji ASP.NET Core API.
/// Odpowiada za konfigurację usług, połączeń, CORS, Swaggera, JWT i uruchomienie aplikacji.
/// </summary>
public static class Program
{
    /// <summary>
    /// Punkt wejścia aplikacji.
    /// </summary>
    /// <param name="args">Argumenty wiersza poleceń.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Konfiguracja kontekstu bazy danych z użyciem PostgreSQL
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Rejestracja kontrolerów MVC oraz Swaggera
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Konfiguracja Swaggera z uwierzytelnianiem JWT
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Contact API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Wpisz swój token pozyskany z api/AuthController/login: **<twój_token>**"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // Rejestracja repozytoriów (infrastruktura)
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();

        // Rejestracja serwisów aplikacyjnych
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        // Rejestracja generatora tokenów JWT (singleton – bezstanu)
        builder.Services.AddSingleton<JwtGenerator>();

        // Konfiguracja autoryzacji opartej na JWT
        builder.Services.AddJwtAuthentication(builder.Configuration);

        // Konfiguracja AutoMappera z profilami mapowań
        builder.Services.AddAutoMapper(typeof(AuthProfile), typeof(ContactProfile));

        // Konfiguracja CORS dla frontendu na localhost:3000
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost3000", policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        // Użycie wybranej polityki CORS
        app.UseCors("AllowLocalhost3000");

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "app v1"));
        }

        // Middleware bezpieczeństwa
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        // Mapowanie kontrolerów
        app.MapControllers();

        // Uruchomienie aplikacji
        app.Run();
    }
}
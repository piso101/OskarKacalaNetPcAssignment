using Microsoft.EntityFrameworkCore;
using BackEnd.Infrastructure.Data; // <-- Jeœli masz ApplicationDbContext tutaj

namespace BackEnd.API;

public class Program
{
    public static void Main(string[] args)
    {
         var builder = WebApplication.CreateBuilder(args);

         // Dodanie DbContext z PostgreSQL
         builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

         builder.Services.AddControllers();
         builder.Services.AddEndpointsApiExplorer();
         builder.Services.AddSwaggerGen();

         var app = builder.Build();

         if (app.Environment.IsDevelopment())
         {
             app.UseDeveloperExceptionPage();
             app.UseSwagger();
             app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "app v1"));
         }

         app.UseHttpsRedirection(); // <-- Dodane dla poprawnoœci
         app.UseAuthorization();
         app.MapControllers();

         app.Run();
    }
}

using Microsoft.EntityFrameworkCore;
using BackEnd.Infrastructure.Data;
using BackEnd.Domain.Interfaces;
using BackEnd.Infrastructure.Repositories;
using Application.Services;
using BackEnd.Application.Services;
using BackEnd.Shared.Helpers;
using BackEnd.Infrastructure.Configurations;

namespace BackEnd.API;

public static class Program
{
    public static void Main(string[] args)
    {
         var builder = WebApplication.CreateBuilder(args);

         builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

         builder.Services.AddControllers();
         builder.Services.AddEndpointsApiExplorer();
         builder.Services.AddSwaggerGen();
         builder.Services.AddScoped<IUserRepository, UserRepository>();
         builder.Services.AddScoped<IContactRepository, ContactRepository>();
         builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
         builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
         builder.Services.AddScoped<IContactService, ContactService>();
         builder.Services.AddScoped<IAuthService, AuthService>();
         builder.Services.AddSingleton<JwtGenerator>();
         builder.Services.AddJwtAuthentication(builder.Configuration);

         var app = builder.Build();

         if (app.Environment.IsDevelopment())
         {
             app.UseDeveloperExceptionPage();
             app.UseSwagger();
             app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "app v1"));
         }
         app.UseHttpsRedirection();
         app.UseAuthentication();
         app.UseAuthorization();
         app.MapControllers();
         app.Run();
    }
}

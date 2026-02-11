using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EmployeeManagement.Data;
using EmployeeManagement.Services.Interfaces;
using EmployeeManagement.Services;
using EmployeeManagement.Mappings;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("FreePolicy", policy =>
            {
                policy.WithOrigins("http://localhost:4200")

                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
        builder.Services.AddScoped<IProjectService, ProjectService>();

        builder.Services.AddScoped<IAuthService, AuthService>();

        // 1. Add Authentication Services
        builder.Services.AddAuthentication(options =>
        {
            // Use the actual constant, not a null object
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "sC9UfQoRxxvisLukKMhzg0F60PNIjAXhbOKJOTb5/TI="))
            };
        });

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

         builder.Services.AddSwaggerGen();
      
    builder.Services.AddDbContext<EmployeeManagementDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeManagementConnectionString")));

        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        builder.Services.AddAutoMapper(typeof(EmployeeProfile));
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

        var app = builder.Build();
        //middleware 
        app.UseRouting();
        app.UseCors("FreePolicy");
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // ORDER MATTERS HERE:
        app.UseAuthentication(); // 1st: Identify the user
        app.UseAuthorization();  // 2nd: Check permissions
        app.MapControllers();
        app.Run();
    }
}
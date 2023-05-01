
using lab_2.BL.Managers.Departement;
using lab_2.BL.Managers.Tickets;
using lab_2.DAL.Data.Context;
using lab_2.DAL.Data.Models;
using lab_2.DAL.Repos.Departments;
using lab_2.DAL.Repos.Tickets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace lab_2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = builder.Configuration.GetConnectionString("Lab2_ConString");
        builder.Services.AddDbContext<DBContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped<ITicketsRepo, TicketsRepo>();
        builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
        builder.Services.AddScoped<IDepartmentsManager, DepartmentsManager> ();
        builder.Services.AddScoped<ITicketsManager, TicketsManager>();

        #region Identity Manager

        builder.Services.AddIdentity<Employee, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;

            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<DBContext>();

        #endregion

        #region Authentication

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Super";
            options.DefaultChallengeScheme = "Super";
        })
            .AddJwtBearer("Super", options =>
            {
                string keyString = builder.Configuration.GetValue<string>("SecretKey") ?? string.Empty;
                var keyInBytes = Encoding.ASCII.GetBytes(keyString);
                var key = new SymmetricSecurityKey(keyInBytes);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        #endregion

        #region Authorization

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admins", policy => policy
                .RequireClaim(ClaimTypes.Role, "Admin"));
            //.RequireClaim(ClaimTypes.NameIdentifier));

            options.AddPolicy("Employees", policy => policy
                .RequireClaim(ClaimTypes.Role, "Employee", "Manager"));
               // .RequireClaim(ClaimTypes.NameIdentifier); 
        });

        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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
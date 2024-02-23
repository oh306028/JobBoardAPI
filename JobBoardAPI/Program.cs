
using JobBoardAPI.Entities;
using JobBoardAPI.Middlwares;
using JobBoardAPI.Services;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace JobBoardAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         
            builder.Services.AddControllers();

            var authenticationSettings = new AuthenticationSettings();

            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });



            builder.Services.AddSingleton(authenticationSettings);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IJobOffertService, JobOffertService>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddScoped<IRequirementService, RequirementService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddDbContext<JobOffertsDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
           

           

            var app = builder.Build();

          
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();

            app.UseHttpsRedirection();
           
            app.UseAuthorization();
            
           

            
            app.MapControllers();

            app.Run();
        }
    }
}

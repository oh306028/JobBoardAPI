
using JobBoardAPI.Middlwares;
using JobBoardAPI.Services;
using JobBoardAPI.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JobBoardAPI
{
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
            builder.Services.AddScoped<IJobOffertService, JobOffertService>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddDbContext<JobOffertsDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
           

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
           
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

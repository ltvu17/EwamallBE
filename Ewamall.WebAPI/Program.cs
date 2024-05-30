using Ewamall.Domain;
using Ewamall.WebAPI.Services;
using Ewamall.WebAPI.Services.Implements;

namespace Ewamall.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetSection("ConnectionStrings").Value;
            builder.Services
                .AddInfrastructure(connectionString)
                .AddDependencyInjection();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials());
            app.AddWebService(connectionString);

            app.MapControllers();

            app.Run();
        }
    }
}
using Ewamall.WebAPI.Services.Implements;
using Ewamall.WebAPI.Services;
using FluentValidation;
using Ewamall.WebAPI.Common;
using FluentValidation.AspNetCore;

namespace Ewamall.WebAPI
{
    public static class WebServiceRegister
    {
        public static string GetConnectionString()
        {
            var rootPath = Directory.GetCurrentDirectory();
            var path = rootPath.Substring(0, rootPath.LastIndexOf("\\"));
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            return config["ConnectionStrings"];
        }
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IIndustryService, IndustryService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddFluentValidation();
            services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod().AllowAnyHeader()
                    .AllowCredentials()
                    ;
                });
            }
            );
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            return services;
        }
    }
}

using Ewamall.WebAPI.Services.Implements;
using Ewamall.WebAPI.Services;
using FluentValidation;
using Ewamall.WebAPI.Common;
using FluentValidation.AspNetCore;
using Ewamall.Domain.IRepository;
using Ewamall.Infrastructure.Repository;
using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Repository;

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
            //Mail
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IIndustryService, IndustryService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped(typeof(IBaseSetupService<>), typeof(BaseSetupService<>));
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

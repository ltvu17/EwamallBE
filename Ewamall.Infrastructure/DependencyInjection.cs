
using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Hubs;
using Ewamall.DataAccess.Repository;
using Ewamall.Domain.IRepository;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ewamall.DataAccess.SubscribeTableDependencies;
using Ewamall.DataAccess.Hubs;
using Microsoft.AspNetCore.Builder;

namespace Ewamall.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EwamallDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
               
            });
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IShipAddressRepo, ShipAddressRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ISellerRepo, SellerRepo>();
            services.AddScoped<IWalletRepo, WalletRepo>();
            services.AddScoped<IVoucherRepo, VoucherRepo>();
            services.AddScoped<IFeedbackRepo, FeedbackRepo>();
            services.AddScoped<INotificationRepo, NotificationRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIndustryAggrateRepo, IndustryAggrateRepo>();
            services.AddScoped<IDashBoardRepo, DashBoardRepo>();
            services.AddScoped<IProductSellDetailRepo, ProductSellDetailRepo>();
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            //
            services.AddSingleton<NotificationHub>();
            //services.AddSingleton<SubscribeNotificationTableDependency>();
            services.AddSignalR();
            return services;
        }
        public static WebApplication AddWebService(this WebApplication app, string connectionString)
        {
            app.MapHub<NotificationHub>("/notificationHub");
           // app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);
            return app;
        }
        public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder, string connectionString)
           where T : ISubscribeTableDependency
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<T>();
            service?.SubscribeTableDependency(connectionString);
        }

    }
}

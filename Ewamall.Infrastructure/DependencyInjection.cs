
using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Repository;
using Ewamall.Domain.IRepository;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIndustryAggrateRepo, IndustryAggrateRepo>();
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            return services;
        }
    }
}

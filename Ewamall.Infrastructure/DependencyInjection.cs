
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIndustryAggrateRepo, IndustryAggrateRepo>();
            return services;
        }
    }
}

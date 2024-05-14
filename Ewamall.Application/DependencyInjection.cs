
using Ewamall.Application.Services;
using Ewamall.Application.Services.Implements;
using Microsoft.Extensions.DependencyInjection;

namespace Ewamall.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            return services;
        }
    }
}

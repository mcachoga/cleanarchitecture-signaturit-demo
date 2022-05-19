using Signaturit.Application.Interfaces.CacheRepositories;
using Signaturit.Application.Interfaces.Contexts;
using Signaturit.Application.Interfaces.Repositories;
using Signaturit.Infrastructure.CacheRepositories;
using Signaturit.Infrastructure.DbContexts;
using Signaturit.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Signaturit.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<ITrialRepository, TrialRepository>();
            services.AddTransient<ITrialCacheRepository, TrialCacheRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #endregion Repositories
        }
    }
}
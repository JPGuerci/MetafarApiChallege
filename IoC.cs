using MetafarApiChallege.Infrastructure.Repositories;
using MetafarApiChallege.Infrastructure.Repositories.Interfaces;
using MetafarApiChallege.Infrastructure.Services;
using MetafarApiChallege.Infrastructure.Services.Interfaces;

namespace MetafarApiChallege
{
    public static class IoC
    {
        public static void AddServices(this IServiceCollection services)
        {
            #region UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddSingleton<IOperationTypeRepository, OperationTypeRepository>();
            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            
            #endregion

            #region Services
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IAccountService, AccountService>();
            #endregion
        }

    }
}

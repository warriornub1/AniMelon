using Microsoft.Extensions.DependencyInjection;
using OneLearn.Application.Common;
using OneLearn.Application.Tests.Interface;
using OneLearn.Application.VoiceTranslation.Interface;
using OneLearn.Infrastructure.Common;
using OneLearn.Infrastructure.Tests;
using OneLearn.Infrastructure.VoiceTranslation;

namespace OneLearn.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IPassageRepository, PassageRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            return services;
        }
    }
}

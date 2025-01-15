using Microsoft.Extensions.DependencyInjection;
using OneLearn.Application.Common;
using OneLearn.Api.Extensions;
using OneLearn.Application.Tests.Services;
using OneLearn.Application.VoiceTranslation.Services;

namespace OneLearn.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddAppplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IPassageService, PassageService>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddScoped<ITestService, TestService>();
            return services;
        }
    }
}

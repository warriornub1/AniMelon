using AniMelon.Application.Video.Services;

namespace AniMelon.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IVideoService, VideoService>();
            return services;
        }
    }
}

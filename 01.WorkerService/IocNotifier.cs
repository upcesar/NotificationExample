using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notification.WorkerService.Component;
using Notification.WorkerService.Options;
using System;

namespace Notification.WorkerService
{
    internal static class IocNotifier
    {
        internal static IServiceCollection ConfigureNotifier<TOption>(this IServiceCollection services, IConfiguration configuration)
            where TOption : class, new()
        {
            var section = $"NotificationServices:{typeof(TOption).Name}".Replace("Settings", string.Empty);
            return services.Configure<TOption>(options => configuration.GetSection(section).Bind(options));
        }

        internal static INotifier WrapNotifiers(this IServiceProvider service)
        {
            INotifier notifier = new WrapperEmailNotifier(service.GetService<IOptions<EmailSettings>>());
            notifier = new WrapperPushNotifier(service.GetService<IOptions<PushSettings>>(), notifier);
            notifier = new WrapperTeamsNotifier(service.GetService<IOptions<TeamsSettings>>(), notifier);

            return notifier;
        }
    }
}

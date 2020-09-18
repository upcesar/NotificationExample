using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Notification.WorkerService.Component;
using Notification.WorkerService.Models;
using Notification.WorkerService.Options;
using System;

namespace Notification.WorkerService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    services.ConfigureNotifier<EmailSettings>(configuration)
                            .ConfigureNotifier<PushSettings>(configuration)
                            .ConfigureNotifier<TeamsSettings>(configuration);

                    services.AddSingleton(service => WrapNotifiers(service));

                    services.AddHostedService<Worker>();
                });

        private static IServiceCollection ConfigureNotifier<TOption>(this IServiceCollection services, IConfiguration configuration)
            where TOption : class, new()
        {
            var section = $"NotificationServices:{typeof(TOption).Name}".Replace("Settings", string.Empty);
            return services.Configure<TOption>(options => configuration.GetSection(section).Bind(options));
        }
        
        private static INotifier WrapNotifiers(IServiceProvider service)
        {
            INotifier notifier = new WrapperEmailNotifier(service.GetService<IOptions<EmailSettings>>());
            notifier = new WrapperPushNotifier(service.GetService<IOptions<PushSettings>>(), notifier);
            notifier = new WrapperTeamsNotifier(service.GetService<IOptions<TeamsSettings>>(), notifier);

            return notifier;
        }

    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification.WorkerService.Options;

namespace Notification.WorkerService
{
    public static class Program
    {
        public static void Main(string[] args) 
            => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) 
            => Host.CreateDefaultBuilder(args)
                   .ConfigureServices((hostContext, services) =>
                   {
                       var configuration = hostContext.Configuration;

                       services.ConfigureNotifier<EmailSettings>(configuration)
                               .ConfigureNotifier<PushSettings>(configuration)
                               .ConfigureNotifier<TeamsSettings>(configuration);

                       services.AddSingleton(service => service.WrapNotifiers());

                       services.AddHostedService<Worker>();
                   });
    }
}

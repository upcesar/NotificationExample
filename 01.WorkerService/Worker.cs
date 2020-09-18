using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Notification.WorkerService.Component;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly INotifier _notifier;

        public Worker(ILogger<Worker> logger, INotifier notifier)
        {
            _logger = logger;
            _notifier = notifier;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _notifier.Send("this message");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

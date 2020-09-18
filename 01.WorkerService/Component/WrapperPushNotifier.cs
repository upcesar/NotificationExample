using Microsoft.Extensions.Options;
using Notification.WorkerService.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.WorkerService.Component
{
    public class WrapperPushNotifier : WrapperBaseNotifier
    {
        private readonly PushSettings _options;

        public WrapperPushNotifier(IOptions<PushSettings> options, INotifier notifier = null) : base(notifier) => _options = options.Value;

        public override Task Send(string message)
        {
            base.Send(message);

            Console.Out.WriteLineAsync($"{message} was sent using {_options.OS} {_options.Version} Push");
            
            return Task.CompletedTask;
        }
    }
}

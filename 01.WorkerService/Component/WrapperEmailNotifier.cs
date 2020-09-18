using Microsoft.Extensions.Options;
using Notification.WorkerService.Options;
using System;
using System.Threading.Tasks;

namespace Notification.WorkerService.Component
{
    public class WrapperEmailNotifier : WrapperBaseNotifier
    {
        private readonly EmailSettings _options;

        public WrapperEmailNotifier(IOptions<EmailSettings> options, INotifier notifier = null) : base(notifier) => _options = options.Value;

        public override Task Send(string message)
        {
            base.Send(message);
            
            Console.Out.WriteLineAsync($"{message} was sent using E-Mail, from server {_options.Host}:{_options.Port}");

            return Task.CompletedTask;
        }        
    }
}

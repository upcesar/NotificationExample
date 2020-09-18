using Microsoft.Extensions.Options;
using Notification.WorkerService.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.WorkerService.Component
{
    public class WrapperTeamsNotifier : WrapperBaseNotifier
    {
        private readonly TeamsSettings _options;

        public WrapperTeamsNotifier(IOptions<TeamsSettings> options, INotifier notifier = null) : base(notifier) => _options = options.Value;

        public override Task Send(string message)
        {
            base.Send(message);
            
            Console.Out.WriteLineAsync($"{message} was sent to {_options.Organization}, chat {_options.ChatName} using Teams!!!");

            return Task.CompletedTask;
        }
    }
}

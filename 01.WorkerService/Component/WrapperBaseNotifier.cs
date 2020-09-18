using System.Threading.Tasks;

namespace Notification.WorkerService.Component
{
    public abstract class WrapperBaseNotifier : INotifier
    {
        private readonly INotifier _notifier;

        protected WrapperBaseNotifier(INotifier notifier) => _notifier = notifier;

        public virtual Task Send(string message) => _notifier?.Send(message);

        public override string ToString() => GetType().Name.Replace("Wrapper", string.Empty);
    }
}

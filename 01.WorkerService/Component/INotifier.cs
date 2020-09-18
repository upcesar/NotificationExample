using System.Threading;
using System.Threading.Tasks;

namespace Notification.WorkerService.Component
{
    public interface INotifier
    {
        Task Send(string message);
    }
}

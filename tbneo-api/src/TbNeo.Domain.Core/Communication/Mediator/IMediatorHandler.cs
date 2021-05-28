using System.Threading.Tasks;
using TbNeo.Domain.Core.Communication.Notifications;

namespace TbNeo.Domain.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task<bool> EnviarComando<T>(T command) where T : Command;

        Task Notificar(Notification notification);
    }
}

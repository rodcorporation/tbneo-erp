using MediatR;
using System.Threading.Tasks;
using TbNeo.Domain.Core.Communication.Notifications;

namespace TbNeo.Domain.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> EnviarComando<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task Notificar(Notification notification)
        {
            await _mediator.Publish(notification);
        }
    }
}

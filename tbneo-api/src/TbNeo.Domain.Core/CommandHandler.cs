using System.Threading.Tasks;
using TbNeo.Domain.Core.Communication.Mediator;
using TbNeo.Domain.Core.Communication.Notifications;

namespace TbNeo.Domain.Core
{
    public abstract class CommandHandler
    {
        protected IMediatorHandler _mediatorHandler;

        public CommandHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> ValidCommand(Command command)
        {
            if (await command.IsValid()) return true;

            foreach(var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.Notificar(new Notification(error.ErrorMessage));
            }

            return false;
        }
    }
}

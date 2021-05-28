using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TbNeo.Domain.Core.Events;
using TbNeo.Domain.Entities;
using TbNeo.Domain.Repositories;

namespace TbNeo.Application.Events.Handlers
{

    public class LogEventHandler : INotificationHandler<DataChangedEvent>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogSistemaRepository _logSistemaRepository;

        public LogEventHandler(IUsuarioRepository usuarioRepository,
                               ILogSistemaRepository logSistemaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _logSistemaRepository = logSistemaRepository;
        }

        public async Task Handle(DataChangedEvent @event, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.Get(@event.IdAlteradoPor);

            var logSistema = new LogSistema(@event.Reference,
                                            @event.NomeCampo,
                                            @event.ValorNovo,
                                            @event.ValorAntigo,
                                            usuario);

            await _logSistemaRepository.Add(logSistema);
            await _logSistemaRepository.UnitOfWork.Commit();
        }
    }
}

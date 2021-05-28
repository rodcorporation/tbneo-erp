using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TbNeo.Api.Controllers;
using TbNeo.Domain.Core.Communication.Mediator;
using TbNeo.Domain.Core.Communication.Notifications;
using TbNeo.Domain.Repositories;

namespace TbNeo.Api.V1.Controllers
{
    [Route("api/v1/log-sistema")]
    public class LogSistemaController : TbNeoControllerBase
    {
        private readonly ILogSistemaRepository _logSistemaRepository;

        public LogSistemaController(ILogSistemaRepository logSistemaRepository,
                                 IMediatorHandler mediatorHandler,
                                 INotificationHandler<Notification> notifications) : base(mediatorHandler,
                                                                                          notifications)
        {
            _logSistemaRepository = logSistemaRepository;
        }

        [HttpGet("{reference:guid}")]
        public async Task<IActionResult> List(Guid reference)
        {
            var logs = await _logSistemaRepository.ListByReference(reference);

            return CustomResponse(logs);
        }
    }
}

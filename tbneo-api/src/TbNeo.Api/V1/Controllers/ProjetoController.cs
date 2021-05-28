using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TbNeo.Api.Controllers;
using TbNeo.Api.V1.InputModels;
using TbNeo.Application.Commands;
using TbNeo.Application.Queries;
using TbNeo.Domain.Core.Communication.Mediator;
using TbNeo.Domain.Core.Communication.Notifications;

namespace TbNeo.Api.V1.Controllers
{
    [Route("api/v1/projeto")]
    public class ProjetoController : TbNeoControllerBase
    {
        private readonly IProjetoQueries _projetoQueries;

        public ProjetoController(IMediatorHandler mediatorHandler,
                                 INotificationHandler<Notification> notifications,
                                 IProjetoQueries projetoQueries) : base(mediatorHandler,
                                                                        notifications)
        {
            _projetoQueries = projetoQueries;
        }

        [HttpGet("")]
        public async Task<IActionResult> List()
        {
            var projetos = await _projetoQueries.Listar();

            return CustomResponse(projetos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var projeto = await _projetoQueries.Detalhes(id);

            return CustomResponse(projeto);
        }

        [HttpPost("")]
        public async Task<IActionResult> Cadastrar(ProjetoCadastrarInputModel inputModel)
        {
            if (this.ModelInvalida()) return CustomResponseErroModelInvalida();

            await _mediatorHandler.EnviarComando(new ProjetoCadastrarCommand(inputModel.Nome,
                                                                             inputModel.Descricao,
                                                                             inputModel.UrlJira,
                                                                             inputModel.IdUsuario));

            return CustomResponse();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Editar(int id,
                                                ProjetoEditarInputModel inputModel)
        {
            if (this.ModelInvalida()) return CustomResponseErroModelInvalida();

            await _mediatorHandler.EnviarComando(new ProjetoEditarCommand(id,
                                                                          inputModel.Nome,
                                                                          inputModel.Descricao,
                                                                          inputModel.UrlJira,
                                                                          inputModel.IdUsuario));

            return CustomResponse();
        }
    }
}

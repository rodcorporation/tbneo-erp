using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/v1/feature-flag")]
    public class FeatureFlagController : TbNeoControllerBase
    {
        private readonly IFeatureFlagQueries _featureFlagQueries;

        public FeatureFlagController(IMediatorHandler mediatorHandler,
                                     INotificationHandler<Notification> notifications,
                                     IFeatureFlagQueries featureFlagQueries) : base(mediatorHandler,
                                                                                    notifications)
        {
            _featureFlagQueries = featureFlagQueries;
        }

        [HttpGet("")]
        public async Task<IActionResult> List()
        {
            var featuresFlag = await _featureFlagQueries.Listar();

            return CustomResponse(featuresFlag);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var featureFlag= await _featureFlagQueries.Detalhes(id);

            return CustomResponse(featureFlag);
        }

        [HttpPost("")]
        public async Task<IActionResult> Cadastrar(FeatureFlagCadastrarInputModel inputModel)
        {
            if (this.ModelInvalida()) return CustomResponseErroModelInvalida();

            await _mediatorHandler.EnviarComando(new FeatureFlagCadastrarCommand(inputModel.Nome,
                                                                                 inputModel.IdProjeto));

            return CustomResponse();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Editar(int id,
                                                FeatureFlagEditarInputModel inputModel)
        {
            if (this.ModelInvalida()) return CustomResponseErroModelInvalida();

            await _mediatorHandler.EnviarComando(new FeatureFlagEditarCommand(id,
                                                                              inputModel.Nome,
                                                                              inputModel.IdProjeto));

            return CustomResponse();
        }
    }
}

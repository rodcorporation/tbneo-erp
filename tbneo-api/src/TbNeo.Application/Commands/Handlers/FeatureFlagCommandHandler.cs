using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TbNeo.Domain.Core;
using TbNeo.Domain.Core.Communication.Mediator;
using TbNeo.Domain.Core.Communication.Notifications;
using TbNeo.Domain.Entities;
using TbNeo.Domain.Repositories;

namespace TbNeo.Application.Commands.Handlers
{
    public class FeatureFlagCommandHandler
                            : CommandHandler,
                              IRequestHandler<FeatureFlagCadastrarCommand, bool>,
                              IRequestHandler<FeatureFlagEditarCommand, bool>
    {
        private IFeatureFlagRepository _featureFlagRepository;
        private IProjetoRepository _projetoRepository;
        private IUsuarioRepository _usuarioRepository;

        public FeatureFlagCommandHandler(IMediatorHandler mediatorHandler,
                                         IFeatureFlagRepository featureFlagRepository,
                                         IUsuarioRepository usuarioRepository,
                                         IProjetoRepository projetoRepository) : base(mediatorHandler)
        {
            _featureFlagRepository = featureFlagRepository;
            _projetoRepository = projetoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(FeatureFlagCadastrarCommand command,
                                        CancellationToken cancellationToken)
        {
            // Fail Fast Validation
            if (!await ValidCommand(command)) return false;

            // Procura o usuario
            var usuario = await _usuarioRepository.Get(command.IdUsuario);

            // Se não existir, notifica;
            if (usuario == null)
            {
                await _mediatorHandler.Notificar(new Notification("O usuário não foi informado não foi encontrado."));

                return false;
            }

            // Procura o projeto
            var projeto = await _projetoRepository.Get(command.IdProjeto);

            // Se não existir, notifica;
            if (projeto == null)
            {
                await _mediatorHandler.Notificar(new Notification("O projeto não foi informado não foi encontrado."));

                return false;
            }

            // Cria uma nova feature flag.
            var featureFlag = new FeatureFlag(command.Nome,
                                              projeto,
                                              usuario);

            // Cadastra uma feature flag;
            await _featureFlagRepository.Add(featureFlag);

            // Commita as mudanças
            await _featureFlagRepository.UnitOfWork.Commit();

            return true;
        }

        public async Task<bool> Handle(FeatureFlagEditarCommand command,
                                       CancellationToken cancellationToken)
        {
            // Fail Fast Validation
            if (!await ValidCommand(command)) return false;

            // Procura o usuario
            var usuario = await _usuarioRepository.Get(command.IdUsuario);

            // Se não existir, notifica;
            if (usuario == null)
            {
                await _mediatorHandler.Notificar(new Notification("O usuário não foi informado não foi encontrado."));

                return false;
            }

            // Procura o projeto
            var projeto = await _projetoRepository.Get(command.IdProjeto);

            // Se não existir, notifica;
            if (projeto == null)
            {
                await _mediatorHandler.Notificar(new Notification("O projeto não foi informado não foi encontrado."));

                return false;
            }

            // Verifica se existe a feature flag;
            var featureFlag = await _featureFlagRepository.Get(command.IdFeatureFlag);

            // Se não existe a feature flag, notifica;
            if (featureFlag == null)
            {
                await _mediatorHandler.Notificar(new Notification("A feature flag não foi informada não foi encontrada."));

                return false;
            }

            // Atualilza os dados da feature flag;
            featureFlag.Alterar(command.Nome,
                                projeto,
                                usuario);

            // Atualilza a feature flag;
            _featureFlagRepository.Update(featureFlag);

            // Commita as mudanças
            await _featureFlagRepository.UnitOfWork.Commit();

            return true;
        }
    }
}

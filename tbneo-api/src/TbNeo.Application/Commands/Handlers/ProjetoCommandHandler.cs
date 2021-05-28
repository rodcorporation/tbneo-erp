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
    public class ProjetoCommandHandler : CommandHandler,
                                         IRequestHandler<ProjetoCadastrarCommand, bool>,
                                         IRequestHandler<ProjetoEditarCommand, bool>
    {
        private IProjetoRepository _projetoRepository;
        private IUsuarioRepository _usuarioRepository;
            
        public ProjetoCommandHandler(IMediatorHandler mediatorHandler,
                                     IProjetoRepository projetoRepository,
                                     IUsuarioRepository usuarioRepository) : base(mediatorHandler)
        {
            _projetoRepository = projetoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(ProjetoCadastrarCommand command,
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

            // Cria um novo projeto.
            var projeto = new Projeto(command.Nome,
                                      command.Descricao,
                                      command.UrlJira,
                                      usuario);

            // Cadastra uma feature flag;
            await _projetoRepository.Add(projeto);

            // Commita as mudanças
            await _projetoRepository.UnitOfWork.Commit();

            return true;
        }

        public async Task<bool> Handle(ProjetoEditarCommand command,
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

            // Atualilza os dados do projeto;
            projeto.Alterar(command.Nome,
                            command.Descricao,
                            command.UrlJira,
                            usuario);

            // Atualilza a usuario;
            _projetoRepository.Update(projeto);

            // Commita as mudanças
            await _projetoRepository.UnitOfWork.Commit();

            return true;
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TbNeo.Domain.Core.Communication.Mediator;
using TbNeo.Domain.Core.Communication.Notifications;

namespace TbNeo.Api.Controllers
{
    [ApiController]
    public abstract class TbNeoControllerBase : ControllerBase
    {
        #region Atributos

        protected IMediatorHandler _mediatorHandler;
        protected NotificationHandler _notifications;

        #endregion

        #region Construtores

        protected TbNeoControllerBase(IMediatorHandler mediatorHandler,
                                      INotificationHandler<Notification> notifications)
        {
            _mediatorHandler = mediatorHandler;
            _notifications = (NotificationHandler)notifications;
        }

        #endregion

        #region Properties

        protected JwtSecurityToken AuthorizationDecryptedToken
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Request.Headers["Authorization"]))
                {
                    var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();

                    return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
                }

                return null;
            }
        }

        protected int UsuarioId
        {
            get
            {
                var jwt = AuthorizationDecryptedToken;

                var claimUsuarioId = jwt.Claims.FirstOrDefault(f => f.Type.Equals("UsuarioId"));

                int.TryParse(claimUsuarioId.Value, out var usuarioId);

                return usuarioId;
            }
        }

        protected string UsuarioEmail
        {
            get
            {
                var jwt = AuthorizationDecryptedToken;

                var claimUsuarioEmail = jwt.Claims.FirstOrDefault(f => f.Type.Equals("UsuarioEmail"));

                var usuarioEmail = Convert.ToString(claimUsuarioEmail.Value);

                return usuarioEmail;
            }
        }

        #endregion

        #region Métodos

        protected IActionResult CustomResponse()
        {
            if (OperacaoValida())
            {
                return Ok();
            }

            return BadRequest(ObterMensagensErro());
        }

        protected IActionResult CustomResponse(object result)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(ObterMensagensErro());
        }

        protected IActionResult CustomResponseErroModelInvalida()
        {
            return BadRequest(ObterMensagensErroModelState());
        }

        protected bool ModelInvalida()
        {
            return !ModelState.IsValid;
        }

        protected IEnumerable<string> ObterMensagensErroModelState()
        {
            return ModelState
                        .Values
                        .SelectMany(p => p.Errors)
                        .Select(p => p.ErrorMessage);
        }

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Mensagem).ToList();
        }

        protected async Task NotificarErro(string mensagem)
        {
            await _mediatorHandler.Notificar(new Notification(mensagem));
            await Task.CompletedTask;
        }

        #endregion
    }
}
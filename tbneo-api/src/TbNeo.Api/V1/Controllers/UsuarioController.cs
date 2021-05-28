using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TbNeo.Api.Controllers;
using TbNeo.Api.V1.InputModels;
using TbNeo.Domain.Core.Communication.Mediator;
using TbNeo.Domain.Core.Communication.Notifications;
using TbNeo.Domain.Entities;
using TbNeo.Domain.Repositories;

namespace TbNeo.Api.V1.Controllers
{
    [Route("api/v1/login")]
    public class UsuarioController : TbNeoControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository,
                                 IMediatorHandler mediatorHandler,
                                 INotificationHandler<Notification> notifications) : base(mediatorHandler,
                                                                                          notifications)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("")]
        public async Task<IActionResult> Login(UsuarioLoginInputModel inputModel)
        {
            await CreateNewUser();

            if (this.ModelInvalida()) return CustomResponseErroModelInvalida();

            // Busca por E-mail
            var usuario = await _usuarioRepository
                                        .GetByEmail(inputModel.Email);

            // Se o usuário n existir, notifica
            if (usuario == null)
            {
                await _mediatorHandler.Notificar(new Notification("Usuário ou senha inválidos"));

                return CustomResponse();
            }

            // Se a senha não for essa, notifica
            if (!usuario.ValidarSenha(inputModel.Senha))
            {
                await _mediatorHandler.Notificar(new Notification("Usuário ou senha inválidos"));

                return CustomResponse();
            }

            // prepara a JWT;
            var claimsJwt = new List<Claim>();

            // Adiciona dos dados do usuário
            claimsJwt.Add(new Claim("UsuarioId", usuario.Id.ToString()));
            claimsJwt.Add(new Claim("UsuarioEmail", usuario.Email));

            // Gera a JWT;
            var tokenDescription = new JwtSecurityToken(issuer: "http://localhost:5000",
                                                        audience: "TbNeoErp",
                                                        expires: DateTime.Now.AddMinutes(60),
                                                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MinhaChaveSuperSecreta")),
                                                                                                    SecurityAlgorithms.HmacSha256Signature),
                                                        claims: claimsJwt);

            var data = new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenDescription)
            };

            return CustomResponse(data);
        }

        [NonAction]
        private async Task CreateNewUser()
        {
            var usuario = new Usuario("Felipe da Costa Souza",
                                      "felipe.souza@gmail.com",
                                      "SenhaSecreta!@2021");

            await _usuarioRepository.Add(usuario);
            await _usuarioRepository.UnitOfWork.Commit();
        }
    }
}

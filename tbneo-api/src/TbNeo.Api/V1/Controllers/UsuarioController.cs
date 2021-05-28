using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TbNeo.Api.Config.AppSettingsModel;
using TbNeo.Api.Controllers;
using TbNeo.Api.V1.InputModels;
using TbNeo.Data;
using TbNeo.Domain.Core.Communication.Mediator;
using TbNeo.Domain.Core.Communication.Notifications;
using TbNeo.Domain.Entities;
using TbNeo.Domain.Repositories;

namespace TbNeo.Api.V1.Controllers
{
    [Route("api/v1/login")]
    public class UsuarioController : TbNeoControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDatabaseMigration _databaseMigration;
        private readonly IOptions<JwtSettings> _jwtSettingsOptions;

        public UsuarioController(IUsuarioRepository usuarioRepository,
                                 IDatabaseMigration databaseMigration,
                                 IMediatorHandler mediatorHandler,
                                 IOptions<JwtSettings> jwtSettingsOptions,
                                 INotificationHandler<Notification> notifications) : base(mediatorHandler,
                                                                                          notifications)
        {
            _usuarioRepository = usuarioRepository;
            _databaseMigration = databaseMigration;
            _jwtSettingsOptions = jwtSettingsOptions;
        }

        [HttpPost("")]
        public async Task<IActionResult> Login(UsuarioLoginInputModel inputModel)
        {
            await _databaseMigration.ApplyMigrations();
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
            var tokenDescription = new JwtSecurityToken(issuer: _jwtSettingsOptions.Value.Issuer,
                                                        audience: _jwtSettingsOptions.Value.Audience,
                                                        expires: DateTime.Now.AddMinutes(_jwtSettingsOptions.Value.TokenLifeTimeInMinutes),
                                                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingsOptions.Value.Key)),
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
            var usuarios = new List<Usuario>()
            {
                new Usuario("Felipe da Costa Souza",
                            "felipe.souza@gmail.com",
                            "SenhaSecreta!@2021"),

                new Usuario("Pedro Henrique Yago Caldeira",
                            "ph_caldeira@yahoo.com.br",
                            "SenhaSecreta!@2021"),

                new Usuario("Giovanni Danilo Galvão",
                            "gidangalvao@outlook.com.br",
                            "SenhaSecreta!@2021"),

                new Usuario("Vitor José Renato Oliveira",
                            "vitor.rolyver@hotmail.com",
                            "SenhaSecreta!@2021"),

                new Usuario("José Eduardo Isaac Viana",
                            "zeviana@ig.com.br",
                            "SenhaSecreta!@2021"),
            };

            foreach (var usuario in usuarios)
            {
                if (await _usuarioRepository.GetByEmail(usuario.Email) == null)
                {
                    await _usuarioRepository.Add(usuario);
                    await _usuarioRepository.UnitOfWork.Commit();
                }
            }
        }
    }
}

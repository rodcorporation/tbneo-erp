using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TbNeo.Application.Queries.QueryModels;
using TbNeo.Domain.Core.Data;

namespace TbNeo.Application.Queries
{
    public class ProjetoQueries : ConnectionBase, IProjetoQueries
    {
        public ProjetoQueries(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<ProjetoListagemQueryModel>> Listar()
        {
            var statement = @"

                                    SELECT          p.id,
                                                    p.nome,
                                                    p.descricao,
                                                    p.UrlJira,
                                                    uc.nome as CriadoPor,
                                                    p.CriadoEm,
                                                    ua.nome as AtualizadoPor,
                                                    p.AtualizadoEm,
                                                    p.idLogReference
                                    FROM            dbo.Projeto p
                                    INNER JOIN      dbo.Usuario uc
                                    ON              p.IdCriadoPor = uc.id
                                    LEFT JOIN      dbo.Usuario ua
                                    ON              p.idAtualizadoPor = ua.id;

                            ";

            using var con = this.GetConnection();

            return await con.QueryAsync<ProjetoListagemQueryModel>(statement);
        }

        public async Task<ProjetoDetalhesQueryModel> Detalhes(int id)
        {
            var statement = @"

                                    SELECT          p.id,
                                                    p.nome,
                                                    p.descricao,
                                                    p.UrlJira,
                                                    uc.nome as CriadoPor,
                                                    p.CriadoEm,
                                                    ua.nome as AtualizadoPor,
                                                    p.AtualizadoEm,
                                                    p.idLogReference
                                    FROM            dbo.Projeto p
                                    INNER JOIN      dbo.Usuario uc
                                    ON              p.IdCriadoPor = uc.id
                                    LEFT JOIN      dbo.Usuario ua
                                    ON              p.idAtualizadoPor = ua.id
                                    WHERE           p.id = @id;

                            ";

            using var con = this.GetConnection();

            return await con.QueryFirstAsync<ProjetoDetalhesQueryModel>(statement, new { id });
        }

    }
}

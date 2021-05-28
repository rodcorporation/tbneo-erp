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

                                    SELECT          *
                                    FROM            dbo.Projeto;

                            ";

            using var con = this.GetConnection();

            return await con.QueryAsync<ProjetoListagemQueryModel>(statement);
        }

        public async Task<ProjetoDetalhesQueryModel> Detalhes(int id)
        {
            var statement = @"

                                    SELECT          *
                                    FROM            dbo.Projeto
                                    WHERE           id = @id;

                            ";

            using var con = this.GetConnection();

            return await con.QueryFirstAsync<ProjetoDetalhesQueryModel>(statement, new { id });
        }

    }
}

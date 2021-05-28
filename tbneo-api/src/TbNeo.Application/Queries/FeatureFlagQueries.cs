using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TbNeo.Application.Queries.QueryModels;
using TbNeo.Domain.Core.Data;

namespace TbNeo.Application.Queries
{
    public class FeatureFlagQueries : ConnectionBase, IFeatureFlagQueries
    {
        public FeatureFlagQueries(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<FeatureFlagListagemQueryModel>> Listar()
        {
            var statement = @"

                                    SELECT          ff.id,
                                                    ff.nome,
                                                    p.id as idProjeto,
                                                    p.nome as nomeProjeto,
                                                    ff.idLogReference
                                    FROM            dbo.FeatureFlag ff
                                    INNER JOIN      dbo.Projeto p
                                    ON              ff.idProjeto = p.Id;

                            ";

            using var con = this.GetConnection();

            return await con.QueryAsync<FeatureFlagListagemQueryModel>(statement);
        }



        public async Task<FeatureFlagDetalhesQueryModel> Detalhes(int id)
        {
            var statement = @"

                                    SELECT          ff.id,
                                                    ff.nome,
                                                    p.id as idProjeto,
                                                    p.nome as nomeProjeto,
                                                    ff.idLogReference
                                    FROM            dbo.FeatureFlag ff
                                    INNER JOIN      dbo.Projeto p
                                    ON              ff.idProjeto = p.Id
                                    WHERE           ff.id = @id;

                            ";

            using var con = this.GetConnection();

            return await con.QueryFirstAsync<FeatureFlagDetalhesQueryModel>(statement, new { id });
        }
    }
}

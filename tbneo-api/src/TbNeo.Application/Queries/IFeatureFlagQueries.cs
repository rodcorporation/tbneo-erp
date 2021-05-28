using System.Collections.Generic;
using System.Threading.Tasks;
using TbNeo.Application.Queries.QueryModels;

namespace TbNeo.Application.Queries
{
    public interface IFeatureFlagQueries
    {
        Task<IEnumerable<FeatureFlagListagemQueryModel>> Listar();

        Task<FeatureFlagDetalhesQueryModel> Detalhes(int id);
    }
}

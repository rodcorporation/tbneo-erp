using System.Collections.Generic;
using System.Threading.Tasks;
using TbNeo.Application.Queries.QueryModels;

namespace TbNeo.Application.Queries
{
    public interface IProjetoQueries
    {
        Task<IEnumerable<ProjetoListagemQueryModel>> Listar();
        Task<ProjetoDetalhesQueryModel> Detalhes(int id);
    }
}

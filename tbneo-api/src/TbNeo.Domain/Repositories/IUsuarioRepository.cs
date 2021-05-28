using System.Threading.Tasks;
using TbNeo.Domain.Core;
using TbNeo.Domain.Entities;

namespace TbNeo.Domain.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario> 
    {
        Task<Usuario> GetByEmail(string email);
    }
}
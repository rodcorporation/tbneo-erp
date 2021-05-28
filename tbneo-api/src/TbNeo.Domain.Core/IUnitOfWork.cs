using System.Threading.Tasks;

namespace TbNeo.Domain.Core
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}

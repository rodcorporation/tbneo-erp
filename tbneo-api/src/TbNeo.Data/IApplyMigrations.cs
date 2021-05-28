using System.Threading.Tasks;

namespace TbNeo.Data
{
    public interface IDatabaseMigration
    {
        Task ApplyMigrations();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TbNeo.Data
{
    public class TbNeoDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TbNeoContext>
    {
        public TbNeoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TbNeoContext>();

            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=TbNeo;Integrated Security=true;");

            return new TbNeoContext(optionsBuilder.Options, null);
        }
    }
}

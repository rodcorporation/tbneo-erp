using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TbNeo.Domain.Core;
using TbNeo.Domain.Entities;

namespace TbNeo.Data
{
    public class TbNeoContext : DbContext, IUnitOfWork, IDatabaseMigration
    {
        #region DbSets
        
        public DbSet<FeatureFlag> FeatureFlags { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        #endregion

        #region Construtores

        public TbNeoContext(DbContextOptions<TbNeoContext> options) : base(options)
        {
        }

        #endregion

        #region Overrides
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(TbNeoContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region IUnitOfWork

        public async Task Commit()
        {
            await this.SaveChangesAsync();
        }

        #endregion

        #region IDatabaseMigration

        public async Task ApplyMigrations()
        {
            if((await Database.GetPendingMigrationsAsync()).Any())
            {
                await Database.MigrateAsync();
            }
        }

        #endregion
    }
}

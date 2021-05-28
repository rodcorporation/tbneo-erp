using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TbNeo.Domain.Core;
using TbNeo.Domain.Core.Communication.Mediator;
using TbNeo.Domain.Entities;

namespace TbNeo.Data
{
    public class TbNeoContext : DbContext, IUnitOfWork, IDatabaseMigration
    {
        #region Atributos

        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region DbSets

        public DbSet<FeatureFlag> FeatureFlags { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<LogSistema> LogSistemas { get; set; }

        #endregion

        #region Construtores

        public TbNeoContext(DbContextOptions<TbNeoContext> options,
                                             IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
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
                    .Ignore<Event>();

            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(TbNeoContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region IUnitOfWork

        public async Task Commit()
        {
            var sucesso = await this.SaveChangesAsync() > 0;

            var domainEntities = ChangeTracker
                                    .Entries<Entity>()
                                    .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

            var domainEvents = domainEntities
                                    .SelectMany(x => x.Entity.Events)
                                    .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                            .Select(async (domainEvent) =>
                            {
                                await _mediatorHandler.PublicarEvento(domainEvent);
                            });

            await Task.WhenAll(tasks);

        }

        #endregion

        #region IDatabaseMigration

        public async Task ApplyMigrations()
        {
            if ((await Database.GetPendingMigrationsAsync()).Any())
            {
                await Database.MigrateAsync();
            }
        }

        #endregion
    }
}

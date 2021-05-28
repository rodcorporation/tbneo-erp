using System;
using TbNeo.Domain.Core;
using TbNeo.Domain.Core.Events;

namespace TbNeo.Domain.Entities
{
    public class FeatureFlag : Entity
    {
        #region Propriedades

        public string Nome { get; private set; }

        public Projeto Projeto { get; private set; }

        public Usuario CriadoPor { get; private set; }

        public DateTime CriadoEm { get; set; }

        public Usuario AtualizadoPor { get; private set; }

        public DateTime? AtualizadoEm { get; set; }

        // For EF Core

        public int IdProjeto { get; private set; }

        public int IdCriadoPor { get; set; }

        public int? IdAtualizadoPor { get; set; }

        #endregion

        #region Construtores

        protected FeatureFlag() { }

        public FeatureFlag(string nome,
                           Projeto projeto,
                           Usuario criadoPor)
        {
            Nome = nome;
            Projeto = projeto;
            IdProjeto = projeto.Id;

            CriadoPor = criadoPor;
            IdCriadoPor = criadoPor.Id;

            CriadoEm = DateTime.Now;
        }

        public void Alterar(string nome,
                            Projeto projeto,
                            Usuario alteradoPor)
        {
            if (!Nome.Equals(nome))
            {
                _events.Add(new DataChangedEvent(this.IdLogReference,
                                                 nameof(Nome),
                                                 nome,
                                                 Nome,
                                                 alteradoPor.Id));
            }

            Nome = nome;

            if (Projeto.Id != projeto.Id)
            {
                _events.Add(new DataChangedEvent(this.IdLogReference,
                                                 nameof(Projeto),
                                                 projeto.Nome,
                                                 Projeto.Nome,
                                                 alteradoPor.Id));
            }

            Projeto = projeto;
            IdProjeto = projeto.Id;

            AtualizadoPor = alteradoPor;
            IdAtualizadoPor = alteradoPor.Id;

            AtualizadoEm = DateTime.Now;
        }

        #endregion
    }
}

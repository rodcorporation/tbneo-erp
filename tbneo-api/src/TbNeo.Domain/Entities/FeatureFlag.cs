using TbNeo.Domain.Core;

namespace TbNeo.Domain.Entities
{
    public class FeatureFlag : Entity
    {
        #region Propriedades

        public string Nome { get; private set; }

        public Projeto Projeto { get; private set; }

        // For EF Core

        public int IdProjeto { get; private set; }

        #endregion

        #region Construtores

        protected FeatureFlag() { }

        public FeatureFlag(string nome,
                           Projeto projeto)
        {
            Nome = nome;
            Projeto = projeto;
            IdProjeto = projeto.Id;
        }

        public void Alterar(string nome,
                            Projeto projeto)
        {
            Nome = nome;
            Projeto = projeto;
            IdProjeto = projeto.Id;
        }

        #endregion
    }
}

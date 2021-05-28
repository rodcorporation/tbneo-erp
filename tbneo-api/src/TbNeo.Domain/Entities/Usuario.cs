using System.Security.Cryptography;
using System.Text;
using TbNeo.Domain.Core;

namespace TbNeo.Domain.Entities
{
    public class Usuario : Entity
    {
        #region Propriedades

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }

        #endregion

        #region Construtores

        protected Usuario() { }

        public Usuario(string nome,
                       string email,
                       string senha)
        {
            Nome = nome;
            Email = email;
            Senha = GerarHashSenha(senha);
        }

        #endregion

        #region Métodos

        public bool ValidarSenha(string senha)
        {
            var senhaAConfirmar = GerarHashSenha(senha);

            return Senha.Equals(senhaAConfirmar);
        }

        private static string GerarHashSenha(string senha)
        {
            using var sha256 = new SHA256Managed();
            var senhaCriptografadaEmBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

            StringBuilder senhaEmHash = new StringBuilder();

            for (int i = 0; i < senhaCriptografadaEmBytes.Length; i++)
            {
                senhaEmHash.Append(senhaCriptografadaEmBytes[i].ToString("x2"));
            }

            return senhaEmHash.ToString();
        }

        #endregion
    }
}

using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TbNeo.Domain.Core;

namespace TbNeo.Application.Commands
{
    public class ProjetoEditarCommand : Command
    {
        public int IdProjeto { get; private set; }

        public string Nome { get; private set; }

        public string Descricao { get; private set; }

        public string UrlJira { get; private set; }

        public int IdUsuario { get; private set; }

        public ProjetoEditarCommand(int idProjeto,
                                    string nome,
                                    string descricao,
                                    string urlJira,
                                    int idUsuario)
        {
            IdProjeto = idProjeto;
            Nome = nome;
            Descricao = descricao;
            UrlJira = urlJira;
            IdUsuario = idUsuario;
        }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ProjetoEditarValidation().ValidateAsync(this);

            return ValidationResult.IsValid;
        }
    }

    public class ProjetoEditarValidation : AbstractValidator<ProjetoEditarCommand>
    {
        public ProjetoEditarValidation()
        {
            RuleFor(p => p.IdProjeto)
                .GreaterThan(0).WithMessage("O projeto precisa ser selecionado.");

            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome precisa ser preenchido")
                .MaximumLength(100).WithMessage("O nome suporta no máximo 100 caracteres");

            RuleFor(p => p.Descricao)
                .Must(p => string.IsNullOrWhiteSpace(p) || (!string.IsNullOrWhiteSpace(p) && p.Length <= 500)).WithMessage("A descrição suporta no máximo 500 caracteres");

            RuleFor(p => p.UrlJira)
                .NotEmpty().WithMessage("A url do jira precisa ser preenchida")
                .MaximumLength(200).WithMessage("A url do jira suporta no máximo 200 caracteres");

            RuleFor(p => p.IdUsuario)
                .GreaterThan(0).WithMessage("O usuário precisa ser selecionado.");
        }
    }
}

using FluentValidation;
using System.Threading.Tasks;
using TbNeo.Domain.Core;

namespace TbNeo.Application.Commands
{
    public class FeatureFlagCadastrarCommand : Command
    {
        public string Nome { get; private set; }

        public int IdProjeto { get; private set; }

        public int IdUsuario { get; private set; }

        public FeatureFlagCadastrarCommand(string nome,
                                           int idProjeto,
                                           int idUsuario)
        {
            Nome = nome;
            IdProjeto = idProjeto;
            IdUsuario = idUsuario;
        }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new FeatureFlagCadastrarValidation().ValidateAsync(this);

            return ValidationResult.IsValid;
        }
    }

    public class FeatureFlagCadastrarValidation : AbstractValidator<FeatureFlagCadastrarCommand>
    {
        public FeatureFlagCadastrarValidation()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O campo nome precisa ser preenchido")
                .MaximumLength(100).WithMessage("O campo nome suporta no máximo 100 caracteres");

            RuleFor(p => p.IdProjeto)
                .GreaterThan(0).WithMessage("O projeto precisa ser selecionado.");

            RuleFor(p => p.IdUsuario)
                .GreaterThan(0).WithMessage("O usuário precisa ser selecionado.");
        }
    }
}

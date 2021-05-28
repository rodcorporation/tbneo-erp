using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;

namespace TbNeo.Domain.Core
{
    public abstract class Command : IRequest<bool>
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract Task<bool> IsValid();
    }
}

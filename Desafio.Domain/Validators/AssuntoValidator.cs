using Desafio.Domain.Entities;
using FluentValidation;

namespace Desafio.Domain.Validators
{
    public class AssuntoValidator : AbstractValidator<Assunto>
    {
        public AssuntoValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Campo {PropertyName} não pode ser vazio");
            RuleFor(x => x.Descricao).NotNull().WithMessage("Campo {PropertyName} não pode ser nulo");
            RuleFor(x => x.Descricao).MaximumLength(40).WithMessage("Campo {PropertyName} deve possuir no máximo 40 caracteres");
        }
    }
}

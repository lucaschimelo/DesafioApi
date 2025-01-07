using Desafio.Domain.Entities;
using FluentValidation;

namespace Desafio.Domain.Validators
{
    public class AutorValidator : AbstractValidator<Autor>
    {
        public AutorValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo {PropertyName} não pode ser vazio");
            RuleFor(x => x.Nome).NotNull().WithMessage("Campo {PropertyName} não pode ser nulo");
            RuleFor(x => x.Nome).MaximumLength(40).WithMessage("Campo {PropertyName} deve possuir no máximo 40 caracteres");
        }
    }
}

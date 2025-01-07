using Desafio.Domain.Entities;
using FluentValidation;
using System;
using System.Linq;

namespace Desafio.Domain.Validators
{
    public class LivroValidator : AbstractValidator<Livro>
    {
        public LivroValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("Campo {PropertyName} não pode ser vazio");
            RuleFor(x => x.Titulo).NotNull().WithMessage("Campo {PropertyName} não pode ser nulo");
            RuleFor(x => x.Titulo).MaximumLength(40).WithMessage("Campo {PropertyName} deve possuir no máximo 40 caracteres");

            RuleFor(x => x.Editora).NotEmpty().WithMessage("Campo {PropertyName} não pode ser vazio");
            RuleFor(x => x.Editora).NotNull().WithMessage("Campo {PropertyName} não pode ser nulo");
            RuleFor(x => x.Editora).MaximumLength(40).WithMessage("Campo {PropertyName} deve possuir no máximo 40 caracteres");

            RuleFor(x => x.AnoPublicacao).NotEmpty().WithMessage("Campo {PropertyName} não pode ser vazio");
            RuleFor(x => x.AnoPublicacao).NotNull().WithMessage("Campo {PropertyName} não pode ser nulo");            
            RuleFor(x => x.AnoPublicacao).Must(isYearValid).WithMessage($"Campo AnoPublicacao deve ser entre 1900 e {DateTime.Now.Year}");

            RuleFor(x => x.Assuntos).Must(x => x.Count() > 0).WithMessage("Obrigatório informar um ou mais assuntos");
            RuleFor(x => x.Autores).Must(x => x.Count() > 0).WithMessage("Obrigatório informar um ou mais autores");
            RuleFor(x => x.FormaCompras).Must(x => x.Count() > 0).WithMessage("Obrigatório informar uma ou mais formas de compra");
        }      

        private bool isYearValid(string valor)
        {
            int.TryParse(valor, out int year);

            return year >= 1900 && year <= DateTime.Now.Year; 
        }
    }
}

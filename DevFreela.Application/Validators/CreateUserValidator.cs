using DevFreela.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateUserValidator :AbstractValidator<CreateUserInputModel>
    {
        public CreateUserValidator()
        {
           
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email não pode ser vazio")
                .EmailAddress().WithMessage("Email em formato inválido");
            RuleFor(u => u.FullName)
                .NotEmpty().WithMessage("Nome completo não pode ser vazio")
                .MaximumLength(100).WithMessage("Tamanho máximo é 100");
            RuleFor(u => u.BirthDate)
                .NotEmpty().WithMessage("Data de nascimento não pode ser vazia")
                .LessThan(DateTime.Now).WithMessage("Data de nascimento deve ser menor que a data atual")
                .GreaterThan(DateTime.Now.AddYears(-150)).WithMessage("Data de nascimento inválida")
                .Must(date => date <= DateTime.Now.AddYears(-18)).WithMessage("Tem que ser maior de 18 anos");
        }
    }
}

using DevFreela.Application.Commands.InsertProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
    {
        public InsertProjectValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Não pode ser vazio")
                .MaximumLength(100).WithMessage("Tamanho máximo é 100");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Não pode ser vazio");
            RuleFor(p => p.IdClient)
                .GreaterThan(0).WithMessage("IdCliente deve ser maior que zero");
            RuleFor(p => p.IdFreelancer)
                .GreaterThan(0).WithMessage("IdFreelancer deve ser maior que zero");
            RuleFor(p => p.TotalCost)
                .GreaterThan(0).WithMessage("TotalCost maior que zero");
        }
    }
}

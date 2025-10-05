using FluentValidation;
using PosSystem.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Validators
{
    public class ValidateCategoryDto: AbstractValidator<CategoryDto>
    {
        public ValidateCategoryDto() 
        {
            RuleFor(c => c.description)
                .NotEmpty().WithMessage("Es necesario especificar la descripción de la categoría.")
                .MaximumLength(50).WithMessage("La descripción de la categoría no debe superar los 50 caracteres.");

            RuleFor(c => c.status)
                .NotEmpty().WithMessage("Es necesario especificar el estado de la categoría.")
                .MaximumLength(8).WithMessage("El estado de la categoría no debe superar los 8 caracteres.")
                .Must(status => status == "Activo" || status == "Inactivo").WithMessage("El estado debe ser 'Activo' o 'Inactivo'.");
        }
    }
}

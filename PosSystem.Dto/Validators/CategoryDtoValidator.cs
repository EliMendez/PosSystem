using FluentValidation;
using PosSystem.Dto.Dto;

namespace PosSystem.Dto.Validators
{
    public class CategoryDtoValidator: AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator() 
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Es necesario especificar la descripción de la categoría.")
                .MaximumLength(50).WithMessage("La descripción de la categoría no debe superar los 50 caracteres.");

            RuleFor(c => c.Status)
                .NotEmpty().WithMessage("Es necesario especificar el estado de la categoría.")
                .MaximumLength(8).WithMessage("El estado de la categoría no debe superar los 8 caracteres.")
                .Must(status => status == "Activo" || status == "Inactivo").WithMessage("El estado debe ser 'Activo' o 'Inactivo'.");
        }
    }
}

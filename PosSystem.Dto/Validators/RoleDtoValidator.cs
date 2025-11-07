using FluentValidation;
using PosSystem.Dto.Dto;

namespace PosSystem.Dto.Validators
{
    public class RoleDtoValidator: AbstractValidator<RoleDto>
    {
        public RoleDtoValidator() 
        {
            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Es necesario especificar la descripción del rol.")
                .MaximumLength(50).WithMessage("La descripción del rol no debe superar los 50 caracteres.");

            RuleFor(r => r.Status)
                .NotEmpty().WithMessage("Es necesario especificar el estado del rol.")
                .MaximumLength(8).WithMessage("El estado del rol no debe superar los 8 caracteres.")
                .Must(status => status == "Activo" || status == "Inactivo").WithMessage("El estado debe ser 'Activo' o 'Inactivo'.");
        }
    }
}

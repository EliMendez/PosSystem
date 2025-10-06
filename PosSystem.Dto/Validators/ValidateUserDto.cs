using FluentValidation;
using PosSystem.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Validators
{
    public class ValidateUserDto: AbstractValidator<UserDto>
    {
        public ValidateUserDto() 
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Es necesario especificar el nombre del usuario.")
                .MaximumLength(35).WithMessage("El nombre del usuario no debe superar los 35 caracteres.");

            RuleFor(u => u.Surname)
                .NotEmpty().WithMessage("Es necesario especificar el apellido del usuario.")
                .MaximumLength(35).WithMessage("El apellido del usuario no debe superar los 35 caracteres.");

            RuleFor(u => u.RoleId)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol válido.");

            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("Es necesario especificar el número de teléfono.")
                .MinimumLength(8).WithMessage("El número de teléfono debe contener al menos 8 caracteres.")
                .MaximumLength(15).WithMessage("El número de teléfono no debe superar los 15 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Es necesario especificar el correo electrónico del usuario.")
                .MaximumLength(50).WithMessage("El correo electrónico del usuario no debe superar los 50 caracteres.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Es necesario especificar la contraseña del usuario.")
                .MinimumLength(8).WithMessage("La contraseña del usuario debe contener al menos 8 caracteres.");

            RuleFor(u => u.Status)
                .NotEmpty().WithMessage("Es necesario especificar el estado del usuario.")
                .MaximumLength(8).WithMessage("El estado del usuario no debe superar los 8 caracteres.")
                .Must(status => status == "Activo" || status == "Inactivo").WithMessage("El estado debe ser 'Activo' o 'Inactivo'.");
        }
    }
}

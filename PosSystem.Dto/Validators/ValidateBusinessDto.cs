using FluentValidation;
using PosSystem.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Validators
{
    public class ValidateBusinessDto : AbstractValidator<BusinessDto>
    {
        public ValidateBusinessDto() 
        {
            RuleFor(b => b.ruc)
                .NotEmpty().WithMessage("Es necesario especificar el número ruc de la empresa.")
                .MaximumLength(20).WithMessage("El número de ruc no debe superar los 20 caracteres.");

            RuleFor(b => b.companyName)
                .NotEmpty().WithMessage("Es necesario especificar el nombre de la empresa.")
                .MaximumLength(50).WithMessage("El nombre de la empresa no debe superar los 50 caracteres.");

            RuleFor(b => b.email)
                .NotEmpty().WithMessage("Es necesario especificar el correo electrónico de la empresa.")
                .MaximumLength(50).WithMessage("El correo electrónico no debe superar los 50 caracteres.");

            RuleFor(b => b.phone)
                .NotEmpty().WithMessage("Es necesario especificar el número de teléfono.")
                .MinimumLength(8).WithMessage("El número de teléfono debe contener al menos 8 caracteres.")
                .MaximumLength(15).WithMessage("El número de teléfono no debe superar los 15 caracteres.");

            RuleFor(b => b.address)
                .NotEmpty().WithMessage("Es necesario especificar la dirección de la empresa.")
                .MaximumLength(500).WithMessage("La dirección no debe superar los 500 caracteres.");

            RuleFor(b => b.owner)
                .NotEmpty().WithMessage("Es necesario especificar el nombre del propietario de la empresa.")
                .MinimumLength(8).WithMessage("El nombre del propietario debe contener al menos 3 caracteres.")
                .MaximumLength(50).WithMessage("El nombre del propietario no debe superar los 50 caracteres.");

            RuleFor(b => b.discount)
                .NotEmpty().WithMessage("Es necesario especificar el descuento.")
                .GreaterThanOrEqualTo(0).WithMessage("El descuento no puede ser menor a cero.");
        }
    }
}

using FluentValidation;
using PosSystem.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PosSystem.Dto.Validators
{
    public class ValidateProductDto : AbstractValidator<ProductDto>
    {
        public ValidateProductDto() 
        {
            RuleFor(p => p.barcode)
                .NotEmpty().WithMessage("Es necesario especificar el código de barra.")
                .MaximumLength(30).WithMessage("El código de barra no debe superar los 30 caracteres.");

            RuleFor(p => p.description)
                .NotEmpty().WithMessage("Es necesario especificar la descripción del producto.")
                .MaximumLength(50).WithMessage("La descripción del producto no debe superar los 50 caracteres.");

            RuleFor(p => p.idCategory)
                .GreaterThan(0).WithMessage("Debe seleccionar una categoría válido.");

            RuleFor(p => p.salePrice)
                .NotEmpty().WithMessage("Es necesario especificar el precio de venta.")
                .GreaterThanOrEqualTo(0).WithMessage("El precio de venta no puede ser menor a cero.");

            RuleFor(p => p.stock)
                .NotEmpty().WithMessage("Es necesario especificar el stock.")
                .GreaterThanOrEqualTo(0).WithMessage("El descuento no puede ser menor a cero.");

            RuleFor(p => p.minimumStock)
                .NotEmpty().WithMessage("Es necesario especificar el stock mínimo.")
                .GreaterThan(0).WithMessage("El stock mínimo no puede ser menor a cero.");

            RuleFor(p => p.status)
                .NotEmpty().WithMessage("Es necesario especificar el estado del producto.")
                .MaximumLength(8).WithMessage("El estado del producto no debe superar los 8 caracteres.")
                .Must(status => status == "Activo" || status == "Inactivo").WithMessage("El estado debe ser 'Activo' o 'Inactivo'.");
        }
    }
}

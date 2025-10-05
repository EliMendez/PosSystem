using FluentValidation;
using PosSystem.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Validators
{
    public class ValidateSaleDetailDto: AbstractValidator<SaleDetailDto>
    {
        public ValidateSaleDetailDto() 
        {
            RuleFor(s => s.idSale)
                .GreaterThan(0).WithMessage("No se ha especificado el ID de la venta.");

            RuleFor(s => s.idProduct)
                .GreaterThan(0).WithMessage("No se ha especificado el ID del producto.");

            RuleFor(s => s.productName)
                .NotEmpty().WithMessage("Es necesario especificar el nombre del producto.")
                .MaximumLength(50).WithMessage("El nombre del producto no debe superar los 50 caracteres.");

            RuleFor(s => s.price)
                .NotEmpty().WithMessage("Es necesario especificar el precio del producto.")
                .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

            RuleFor(s => s.count)
                .NotEmpty().WithMessage("Es necesario especificar la cantidad de venta.")
                .GreaterThan(0).WithMessage("La cantidad de la venta no puede ser mayor a cero.");

            RuleFor(s => s.discount)
                .GreaterThanOrEqualTo(0).WithMessage("El descuento no puede ser a menor a cero.");

            RuleFor(s => s.total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a cero.");
        }
    }
}

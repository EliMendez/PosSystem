using FluentValidation;
using PosSystem.Dto.Dto;

namespace PosSystem.Dto.Validators
{
    public class SaleDetailDtoValidator: AbstractValidator<SaleDetailDto>
    {
        public SaleDetailDtoValidator() 
        {
            RuleFor(s => s.SaleId)
                .GreaterThan(0).WithMessage("No se ha especificado el ID de la venta.");

            RuleFor(s => s.ProductId)
                .GreaterThan(0).WithMessage("No se ha especificado el ID del producto.");

            RuleFor(s => s.ProductName)
                .NotEmpty().WithMessage("Es necesario especificar el nombre del producto.")
                .MaximumLength(50).WithMessage("El nombre del producto no debe superar los 50 caracteres.");

            RuleFor(s => s.Price)
                .NotEmpty().WithMessage("Es necesario especificar el precio del producto.")
                .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

            RuleFor(s => s.Quantity)
                .NotEmpty().WithMessage("Es necesario especificar la cantidad de venta.")
                .GreaterThan(0).WithMessage("La cantidad de la venta no puede ser mayor a cero.");

            RuleFor(s => s.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("El descuento no puede ser a menor a cero.");

            RuleFor(s => s.Total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a cero.");
        }
    }
}

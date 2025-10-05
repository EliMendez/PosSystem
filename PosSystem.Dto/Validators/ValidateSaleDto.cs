using FluentValidation;
using PosSystem.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Validators
{
    public class ValidateSaleDto: AbstractValidator<SaleDto>
    {
        public ValidateSaleDto() 
        {
            RuleFor(s => s.dni)
                .NotEmpty().WithMessage("Es necesario especificar el número de identifición del cliente.")
                .MaximumLength(20).WithMessage("El número de identifición no debe superar los 20 caracteres.");

            RuleFor(s => s.customer)
                .NotEmpty().WithMessage("Es necesario especificar el nombre del cliente.")
                .MinimumLength(3).WithMessage("El nombre del cliente debe tener al menos 3 caracteres.")
                .MaximumLength(50).WithMessage("El nombre del cliente no debe superar los 50 caracteres.");

            RuleFor(s => s.discount)
                .GreaterThanOrEqualTo(0).WithMessage("El descuento de la venta no puede ser menor a cero.");

            RuleFor(s => s.total)
                .GreaterThan(0).WithMessage("El total de la venta debe ser mayor a cero.");

            RuleFor(s => s.idUser)
                .GreaterThan(0).WithMessage("No se ha especificado el usuario que realizó la venta.");

            RuleFor(s => s.status)
                .IsInEnum().WithMessage("El estado de la venta no es válido. Seleccione una opción de la lista desplegable.");

            RuleFor(s => s.annulledDate)
                .Null().When(s => s.status == Model.Model.SaleStatus.Active).WithMessage("La fecha de anulación debe ser nula si el estado de la venta es: 'Activa'.")
                .NotNull().When(s => s.status == Model.Model.SaleStatus.Annulled).WithMessage("La fecha de anulación es requerida si el estado de la venta es: 'Anulada'.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).When(s => s.annulledDate.HasValue).WithMessage("La fecha de anulación no puede ser mayor a la fecha actual.");

            RuleFor(s => s.reason)
                .NotEmpty().When(s => s.status == Model.Model.SaleStatus.Annulled).WithMessage("El motivo de anulación de la venta es requerido cuando la venta es: 'Anulada'.");

            RuleFor(s => s.userCancel)
                .NotEmpty().When(s => s.status == Model.Model.SaleStatus.Annulled).WithMessage("El usuario que anula la venta es requerido si el estado de la venta es: 'Anulada'.");
        }
    }
}

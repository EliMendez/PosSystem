using FluentValidation;
using PosSystem.Dto.Dto;

namespace PosSystem.Dto.Validators
{
    public class CancelSaleDtoValidator : AbstractValidator<CancelSaleDto>
    {
        public CancelSaleDtoValidator()
        {
            RuleFor(s => s.Reason)
                .NotEmpty().WithMessage("El motivo de anulación de la venta es requerida.");

            RuleFor(s => s.UserId)
                .GreaterThan(0).WithMessage("No se ha especificado el usuario que realizó la venta.");
        }
    }
}

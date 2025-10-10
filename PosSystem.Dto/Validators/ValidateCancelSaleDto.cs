using FluentValidation;
using PosSystem.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Validators
{
    public class ValidateCancelSaleDto : AbstractValidator<CancelSaleDto>
    {
        public ValidateCancelSaleDto()
        {
            RuleFor(s => s.Reason)
                .NotEmpty().WithMessage("El motivo de anulación de la venta es requerida.");

            RuleFor(s => s.UserId)
                .GreaterThan(0).WithMessage("No se ha especificado el usuario que realizó la venta.");
        }
    }
}

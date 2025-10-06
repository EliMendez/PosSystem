using FluentValidation;
using PosSystem.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Validators
{
    public class ValidateDocumentNumberDto : AbstractValidator<DocumentNumberDto>
    {
        public ValidateDocumentNumberDto() 
        {
            RuleFor(dn => dn.Document)
                .NotEmpty().WithMessage("Es necesario especificar él número de documento.")
                .MaximumLength(10).WithMessage("El número de documento no debe superar los 10 caracteres.");
        }
    }
}

using FluentValidation;
using PosSystem.Dto.Dto;

namespace PosSystem.Dto.Validators
{
    public class DocumentNumberDtoValidator : AbstractValidator<DocumentNumberDto>
    {
        public DocumentNumberDtoValidator() 
        {
            RuleFor(dn => dn.Document)
                .NotEmpty().WithMessage("Es necesario especificar él número de documento.")
                .MaximumLength(10).WithMessage("El número de documento no debe superar los 10 caracteres.");
        }
    }
}

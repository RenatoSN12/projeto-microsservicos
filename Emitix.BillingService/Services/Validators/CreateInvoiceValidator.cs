using Emitix.BillingService.DTOs.Requests;
using FluentValidation;

namespace Emitix.BillingService.Services.Validators;

public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceDto>
{
    public CreateInvoiceValidator()
    {
        RuleFor(x => x.Number)
            .GreaterThan(0).WithMessage("O número da nota fiscal é obrigatório.");

        RuleFor(x => x.Series)
            .NotEmpty().WithMessage("A série da nota fiscal é obrigatória.");

        RuleFor(x => x.Products)
            .NotEmpty().WithMessage("Para gerar uma nota fiscal, é obrigatório víncular ao menos um item.");

        RuleForEach(x => x.Products)
            .SetValidator(new CreateInvoiceProductValidator());
    }
}
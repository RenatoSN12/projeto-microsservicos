using Emitix.BillingService.DTOs.Requests.InvoiceProduct;
using FluentValidation;

namespace Emitix.BillingService.Services.Validators;

public class CreateInvoiceProductValidator : AbstractValidator<CreateInvoiceProductDto>
{
    public CreateInvoiceProductValidator()
    {
        RuleFor(x=> x.ProductCode)
            .NotEmpty().WithMessage("O código do produto deve ser informado");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade do produto deve ser maior que zero.");
        
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("O preço unitário do produto deve ser maior que zero.");
    }
}
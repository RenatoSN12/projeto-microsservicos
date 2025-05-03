using Emitix.StockService.DTOs.Requests;
using FluentValidation;

namespace Emitix.StockService.Services.Validators;

public class UpdateProductStockValidator : AbstractValidator<UpdateProductStockDto>
{
    public UpdateProductStockValidator()
    {
        RuleFor(x => x.ProductCode)
            .NotEmpty().WithMessage("Para realizar uma movimentação no estoque, é necessário informar o código do produto.");
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Para realizar uma movimentação no estoque, a quantidade deve ser maior que zero.");
    }
}
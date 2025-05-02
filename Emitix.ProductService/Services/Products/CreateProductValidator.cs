using Emitix.ProductService.DTOs.Requests;
using FluentValidation;

namespace Emitix.ProductService.Services.Products;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x=> x.Code)
            .NotEmpty().WithMessage("É obrigatório informar um código para o novo produto.")
            .MaximumLength(20).WithMessage("O código do produto deve possuir no máximo 20 caracteres.");
        
        RuleFor(x=> x.Description)
            .MaximumLength(255).WithMessage("A descrição do produto deve possuir no máximo 255 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("O preço do produto deve ser maior ou igual a zero.");
    }
}
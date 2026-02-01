using FluentValidation;

namespace PrintProduct.Application.Features.Product.Commands.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        //RuleFor(x => x.Product.Name)
        //    .NotEmpty().WithMessage("Name is required")
        //    .MaximumLength(100);

        //RuleFor(x => x.Product.Price)
        //    .GreaterThan(0).WithMessage("Price must be greater than zero");

        //RuleFor(x => x.Product.Stock)
        //    .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
    }
}

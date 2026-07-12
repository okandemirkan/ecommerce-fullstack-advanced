using Application.Extensions;
using Application.Features.Products.Commands;
using FluentValidation;

namespace Application.Validators.ProductValidators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(p => p.Product.ImageUrl).ImageUrlValidation();
        }
    }
}

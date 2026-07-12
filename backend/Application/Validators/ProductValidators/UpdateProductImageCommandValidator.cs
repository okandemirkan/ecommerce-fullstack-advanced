using Application.Extensions;
using Application.Features.Products.Commands;
using FluentValidation;

namespace Application.Validators.ProductValidators
{
    public class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageCommand>
    {
        public UpdateProductImageCommandValidator()
        {
            RuleFor(p => p.imageUrl).ImageUrlValidation();
        }
    }
}

using Application.Extensions;
using Application.Features.Products.Commands;
using FluentValidation;

namespace Application.Validators.ProductValidators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.productDTO.ImageUrl).ImageUrlValidation();
        }
    }
}

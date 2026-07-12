using Application.Exceptions;
using Application.Features.Products.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Handlers.Command
{
    public class RemoveProductImageHandler : IRequestHandler<RemoveProductImageCommand, Result<object>>
    {
        private readonly IProductRepository _productRepository;

        public RemoveProductImageHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<object>> Handle(RemoveProductImageCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.productId);
            if (product is null)
                throw new NotFoundException("No product found with provided Id");

            product.RemoveProductImage();
            await _productRepository.UpdateProduct(product);

            return Result<object>.Success("Product image removed successfully");

        }
    }
}

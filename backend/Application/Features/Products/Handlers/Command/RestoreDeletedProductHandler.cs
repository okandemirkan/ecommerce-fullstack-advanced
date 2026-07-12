using Application.Exceptions;
using Application.Features.Products.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Handlers.Command
{
    public class RestoreDeletedProductHandler : IRequestHandler<RestoreDeletedProductCommand,
        Result<object>>
    {
        private readonly IProductRepository _productRepository;

        public RestoreDeletedProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<object>> Handle(RestoreDeletedProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAnyProductById(request.productId);
            if (product is null)
                throw new NotFoundException("No soft deleted product found with provided Id");

            product.Restore();
            await _productRepository.UpdateProduct(product);
            return Result<object>.Success("Product restored successfully.");

        }
    }
}

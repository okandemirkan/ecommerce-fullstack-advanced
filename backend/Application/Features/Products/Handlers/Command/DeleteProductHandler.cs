using Application.Exceptions;
using Application.Features.Products.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Handlers.Command
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Result<object>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public DeleteProductHandler(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Result<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAnyProductById(request.productId);
            if (product is null)
                throw new NotFoundException("No product found with provided Id");

            var hasorder = await _orderRepository.IsProductUsedInAnyOrder(product.Id);
            if (hasorder)
                throw new BadRequestException("This product cannot be permanently deleted because it is associated with an order. " +
                    "You can deactivate it instead.");

            await _productRepository.DeleteProduct(product);

            return Result<object>.Success("Product deleted successfully");
        }
    }
}

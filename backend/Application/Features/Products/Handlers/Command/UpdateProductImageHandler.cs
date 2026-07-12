using Application.DTOs.ProductDTOs;
using Application.Exceptions;
using Application.Features.Products.Commands;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;
                                        
namespace Application.Features.Products.Handlers.Command
{
    public class UpdateProductImageHandler : IRequestHandler<UpdateProductImageCommand, Result<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public UpdateProductImageHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        public async Task<Result<ProductResponseDTO>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.productId);
            if (product is null)
                throw new NotFoundException("No product found with provided Id");

            product.UpdateImage(request.imageUrl);
            await _productRepository.UpdateProduct(product);

            var result = _mapper.Map<ProductResponseDTO>(product);

            return Result<ProductResponseDTO>.Success("Product image updated successfully",result);
        }
    }
}

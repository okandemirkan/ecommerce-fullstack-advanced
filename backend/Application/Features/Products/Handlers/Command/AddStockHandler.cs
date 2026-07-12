using Application.DTOs.ProductDTOs;
using Application.Exceptions;
using Application.Features.Products.Commands;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Handlers.Command
{
    public class AddStockHandler : IRequestHandler<AddStockCommand, Result<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public AddStockHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProductResponseDTO>> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.Id);
            if (product is null)
                throw new NotFoundException("No product found with the provided id.");

            product.AddStock(request.Quantity);
            await _productRepository.UpdateProduct(product);

            var result = _mapper.Map<ProductResponseDTO>(product);

            return Result<ProductResponseDTO>.Success("Stock added successfully",result);
        }
    }
}

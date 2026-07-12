using Application.DTOs.ProductDTOs;
using Application.Exceptions;
using Application.Features.Products.Queries;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Handlers.Get
{
    public class GetSoftDeletedProductByIdHandler : IRequestHandler<GetSoftDeletedProductByIdQuery
        , Result<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetSoftDeletedProductByIdHandler(IProductRepository productRepository, 
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProductResponseDTO>> Handle(GetSoftDeletedProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetSoftDeletedProductById(request.productId);
            if (product is null)
                throw new NotFoundException("No soft deleted product found with provided Id");
        
            var result = _mapper.Map<ProductResponseDTO>(product);

            return Result<ProductResponseDTO>.Success("Soft deleted product retrieved successfully."
                ,result);
        }
    }
}

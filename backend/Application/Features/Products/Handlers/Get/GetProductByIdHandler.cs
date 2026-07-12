using Application.DTOs.ProductDTOs;
using Application.Exceptions;
using Application.Features.Products.Queries;
using Application.Interfaces;
using Application.Result;
using MediatR;
using AutoMapper;
namespace Application.Features.Products.Handlers.Get
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, 
        Result<ProductResponseDTO>>
    {
        private IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetProductByIdHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<ProductResponseDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductById(request.Id);
            if (product == null)
                throw new NotFoundException("No product found with the provided Id");

            var result = _mapper.Map<ProductResponseDTO>(product);

            return Result<ProductResponseDTO>.Success("Product retrieved successfully."
                , result);
        }
    }
}

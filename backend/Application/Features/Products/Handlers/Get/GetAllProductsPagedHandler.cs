using Application.Features.Products.Queries;
using Application.Interfaces;
using MediatR;
using Application.Result;
using Application.DTOs.ProductDTOs;
using Application.Pagination;
using AutoMapper;

namespace Application.Features.Products.Handlers.Get
{
    public class GetAllProductsPagedHandler : IRequestHandler<GetAllProductsPagedQuery,
        Result<PagedList<ProductResponseDTO>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsPagedHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Result<PagedList<ProductResponseDTO>>> Handle(GetAllProductsPagedQuery request, CancellationToken cancellationToken)
        {
            var pagedProducts = await _productRepository.GetAllProductsPaged(request.sortType,request.pageNumber,request.pageSize);

            var mappedItems = _mapper.Map<List<ProductResponseDTO>>(pagedProducts.Items);

            var result = new PagedList<ProductResponseDTO>(mappedItems, pagedProducts.TotalCount,
                request.pageNumber, request.pageSize);

            return Result<PagedList<ProductResponseDTO>>
                .Success("Products retrieved successfully.", result);
        }
    }
}

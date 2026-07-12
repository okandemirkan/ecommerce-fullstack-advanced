using Application.DTOs.ProductDTOs;
using Application.Features.Products.Queries;
using Application.Interfaces;
using Application.Pagination;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Handlers.Get
{
    public class GetSoftDeletedProductsHandler : IRequestHandler<GetSoftDeletedProductsPagedQuery,
        Result<PagedList<ProductResponseDTO>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetSoftDeletedProductsHandler(IProductRepository productRepository, 
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Result<PagedList<ProductResponseDTO>>> Handle(GetSoftDeletedProductsPagedQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetSoftDeletedProducts(request.pageNumber,request.pageSize);

            var mappedItems = _mapper.Map<List<ProductResponseDTO>>(products.Items);

            var result = new PagedList<ProductResponseDTO>(mappedItems,products.TotalCount,
                request.pageNumber,request.pageSize);

            return Result<PagedList<ProductResponseDTO>>.
                Success("Soft deleted products retrieved successfully",result);
        }
    }
}

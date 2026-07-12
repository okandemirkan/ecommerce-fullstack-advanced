using Application.DTOs.ProductDTOs;
using Application.Features.Products.Queries;
using Application.Interfaces;
using Application.Pagination;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Handlers.Get
{
    public class SearchProductsByNameHandler : IRequestHandler<SearchProductsByNameQuery,
        Result<PagedList<ProductResponseDTO>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public SearchProductsByNameHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<ProductResponseDTO>>> Handle(SearchProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var products = request.asAdmin ? await _productRepository.SearchProductsByNameAsAdmin(request.sortType, request.productName, 
                request.pageNumber,request.pageSize) :  
                await _productRepository.SearchProductsByName(request.sortType,request.productName, 
                request.pageNumber,request.pageSize);

            var mappedProducts = _mapper.Map<List<ProductResponseDTO>>(products.Items);

            var result = new PagedList<ProductResponseDTO>(mappedProducts,products.TotalCount,request.pageNumber,
                request.pageSize);

            return Result<PagedList<ProductResponseDTO>>.Success("Products retrived Successfully",result);
        }
    }
}

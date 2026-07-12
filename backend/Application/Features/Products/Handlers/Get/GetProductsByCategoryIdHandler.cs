using Application.DTOs.ProductDTOs;
using Application.Exceptions;
using Application.Features.Products.Queries;
using Application.Interfaces;
using Application.Pagination;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Handlers.Get
{
    public class GetProductsByCategoryIdHandler : IRequestHandler<GetProductsByCategoryIdQuery,
        Result<PagedList<ProductResponseDTO>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetProductsByCategoryIdHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, 
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<ProductResponseDTO>>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryById(request.categoryId);
            if (category is null)
                throw new NotFoundException("No category found with provided Id");

            var products = await _productRepository.GetProductsByCategoryId(request.sortType,request.categoryId,request.pageNumber,
                request.pageSize);

            var mappedItems = _mapper.Map<List<ProductResponseDTO>>(products.Items);

            var result = new PagedList<ProductResponseDTO>(mappedItems, products.TotalCount, request.pageNumber, 
                request.pageSize);

            return Result<PagedList<ProductResponseDTO>>.
                Success($"{category.CategoryName} products retrieved successfully", result);
        }
    }
}

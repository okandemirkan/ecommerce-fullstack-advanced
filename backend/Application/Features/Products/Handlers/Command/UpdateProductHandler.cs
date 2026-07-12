using Application.DTOs.ProductDTOs;
using Application.Exceptions;
using Application.Features.Products.Commands;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Handlers.Command
{
    public class UpdateProductHandler : 
        IRequestHandler<UpdateProductCommand,Result<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public UpdateProductHandler(IProductRepository repository, ICategoryRepository categoryRepository, 
            IMapper mapper)
        {
            _productRepository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProductResponseDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.id);
            if (product is null)
                throw new NotFoundException("No product found with the provided ID");
            var dto = request.productDTO;

            var category = await _categoryRepository.GetCategoryById(dto.categoryId);
            if (category is null)
                throw new NotFoundException("No category found with provieded Id");

            product.UpdateProduct(dto.ProductName,dto.categoryId,dto.Description,dto.ImageUrl,dto.Price,dto.Stock);
            await _productRepository.UpdateProduct(product);

            var result = new ProductResponseDTO(product.Id, product.ProductName,dto.categoryId,category.CategoryName, 
                product.Description, product.ImageUrl,product.Price, product.Stock);

            return Result<ProductResponseDTO>.Success("Product updated successfully.",
                result);
        }
    }
}

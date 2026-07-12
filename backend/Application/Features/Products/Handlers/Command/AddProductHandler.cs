using Application.DTOs.ProductDTOs;
using Application.Exceptions;
using Application.Features.Products.Commands;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using Domain.Entities;
using MediatR;
namespace Application.Features.Products.Handlers.Command
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Result<ProductResponseDTO>>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public AddProductHandler(IProductRepository repository,
            ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProductResponseDTO>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Product;
            var category = await _categoryRepository.GetCategoryById(dto.categoryId);
            if (category is null)
                throw new NotFoundException("No category found with provided Id");

            var newProduct = Product.AddProduct(dto.ProductName,dto.categoryId,dto.Description,dto.ImageUrl,
                dto.Price,dto.Stock);

            await _repository.AddProduct(newProduct);

            var result = new ProductResponseDTO(newProduct.Id,newProduct.ProductName,newProduct.CategoryId,
                newProduct.Category.CategoryName,newProduct.Description,newProduct.ImageUrl,newProduct.Price,
                newProduct.Stock);

            return Result<ProductResponseDTO>.Success("Product created successfully.", result);
        }
    }
}

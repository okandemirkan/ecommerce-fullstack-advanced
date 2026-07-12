using Application.DTOs.CartItemDTOs;
using Application.Exceptions;
using Application.Features.CartItems.Commands;
using Application.Interfaces;
using Application.Result;
using Domain.Entities;
using MediatR;

namespace Application.Features.CartItems.Handlers
{
    public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, Result<CartItemDTO>>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        public AddCartItemHandler(ICartItemRepository cartItemRepository, 
            IProductRepository productRepository)
        {
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<CartItemDTO>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CartItem;
            var product = await _productRepository.GetProductById(dto.productId);
            if (product is null)
                throw new NotFoundException("No product found with provided Id");
            if (product.Stock < dto.quantity)
                throw new BadRequestException("Insufficient stock");

            var existingCartItem = await _cartItemRepository.GetCartItemByProductId(request.userId,dto.productId);

            if (existingCartItem is null)
            {
                var cartItem = CartItem.CreateCart(request.userId, product, dto.quantity);
                await _cartItemRepository.AddCartItem(cartItem);

                var result = new CartItemDTO(cartItem.Id,cartItem.Product.Id ,cartItem.Product.ProductName,
                    cartItem.Product.ImageUrl,cartItem.Product.Price, cartItem.Quantity, cartItem.TotalPrice);

                return Result<CartItemDTO>.Success("Cart item added succesfully", result);
            }
            else
            {
                existingCartItem.AddQuantity(dto.quantity);
                await _cartItemRepository.UpdateCartItem(existingCartItem);

                var result = new CartItemDTO(existingCartItem.Id,existingCartItem.Product.Id ,existingCartItem.Product.ProductName,
                    existingCartItem.Product.ImageUrl,existingCartItem.Product.Price, existingCartItem.Quantity, existingCartItem.TotalPrice);
                return Result<CartItemDTO>.Success("Cart item quantity increased successfully.", result);
            }

        }
    }
}

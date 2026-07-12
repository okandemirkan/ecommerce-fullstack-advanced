using Application.DTOs.CartItemDTOs;
using Application.Exceptions;
using Application.Features.CartItems.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.CartItems.Handlers
{
    public class UpdateQuantityHandler : IRequestHandler<UpdateQuantityCommand, Result<CartItemDTO>>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public UpdateQuantityHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }
        public async Task<Result<CartItemDTO>> Handle(UpdateQuantityCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _cartItemRepository.GetCartItemById(request.cartItemId);
            if (cartItem is null)
                throw new NotFoundException("No cart item found with provided Id");

            cartItem.UpdateQuantity(request.quantity);
            await _cartItemRepository.UpdateCartItem(cartItem);

            var result = new CartItemDTO(cartItem.Id,cartItem.Product.Id ,cartItem.Product.ProductName,
                cartItem.Product.ImageUrl,cartItem.Product.Price,request.quantity, cartItem.Product.Price*request.quantity);

            return Result<CartItemDTO>.Success("Cart item quantity updated successfully", result);
        }
    }
}

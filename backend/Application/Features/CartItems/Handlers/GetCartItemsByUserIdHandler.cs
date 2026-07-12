using Application.DTOs.CartItemDTOs;
using Application.Exceptions;
using Application.Features.CartItems.Queries;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.CartItems.Handlers
{
    public class GetCartItemsByUserIdHandler : IRequestHandler<GetCartItemsByUserIdQuery, Result<CartDTO>>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public GetCartItemsByUserIdHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Result<CartDTO>> Handle(GetCartItemsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartItemRepository.GetCartItemsWithProductsByUserId(request.userId);
            if (cartItems is null)
                throw new NotFoundException("No cart item found with provided Id");

            var items = cartItems.Select(c => new CartItemDTO(c.Id,c.Product.Id, c.Product.ProductName,
                c.Product.ImageUrl,c.Product.Price, c.Quantity, c.TotalPrice));

            var grandTotal = items.Sum(i => i.totalPrice);
            
            var result = new CartDTO(items,grandTotal);

            return Result<CartDTO>.Success("Cart items retrieved successfully.",result);
        }
    }
}

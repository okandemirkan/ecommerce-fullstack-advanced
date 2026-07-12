using Application.Exceptions;
using Application.Features.CartItems.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.CartItems.Handlers
{
    public class ClearCartItemsHandler : IRequestHandler<ClearCartItemsCommand, Result<object>>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public ClearCartItemsHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Result<object>> Handle(ClearCartItemsCommand request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartItemRepository.GetCartItemsByUserId(request.userId);
            if (cartItems is null || !cartItems.Any())
                throw new NotFoundException("Cart items not found");

            await _cartItemRepository.ClearCart(cartItems);
            return Result<object>.Success("Cart cleared successfully");
        }
    }
}

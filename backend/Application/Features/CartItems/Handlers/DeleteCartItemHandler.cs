using Application.Exceptions;
using Application.Features.CartItems.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.CartItems.Handlers
{
    public class DeleteCartItemHandler : IRequestHandler<DeleteCartItemCommand, Result<object>>
    {
        private readonly ICartItemRepository _cartItemRepository;
        public DeleteCartItemHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Result<object>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartItemRepository.GetCartItemsWithProductsByUserId(request.userId);
            if (cartItems is null)
                throw new NotFoundException("no cart items found with provided User Id");

           var cartItem = cartItems.FirstOrDefault(c => c.Id == request.cartItemId);
            if (cartItem is null)
                throw new NotFoundException("no cart items found with provided cart item Id");

            await _cartItemRepository.DeleteCartItem(cartItem);

            return Result<object>.Success("Cart item deleted successfully.");
        }
    }
}

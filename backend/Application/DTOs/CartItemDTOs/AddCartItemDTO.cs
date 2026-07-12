using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.CartItemDTOs
{
    public record AddCartItemDTO(int productId,int quantity);
}

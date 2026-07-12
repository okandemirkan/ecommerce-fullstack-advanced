using Application.DTOs.AddressDTOs;
using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;
using Application.DTOs.ProductDTOs;
using Application.DTOs.ReviewDTOs;
using Application.DTOs.UserDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseDTO>()
                .ForCtorParam("productId", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("productName", opt => opt.MapFrom(src => src.ProductName))
                .ForCtorParam("categoryId", opt => opt.MapFrom(src => src.CategoryId))
                .ForCtorParam("categoryName", opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForCtorParam("description", opt => opt.MapFrom(src => src.Description))
                .ForCtorParam("imageUrl", opt => opt.MapFrom(src => src.ImageUrl))
                .ForCtorParam("price", opt => opt.MapFrom(src => src.Price))
                .ForCtorParam("stock", opt => opt.MapFrom(src => src.Stock));

            CreateMap<Address, AddressDTO>()
                .ForCtorParam("City", opt => opt.MapFrom(src => src.City))
                .ForCtorParam("District", opt => opt.MapFrom(src => src.District))
                .ForCtorParam("FullAddress", opt => opt.MapFrom(src => src.FullAddress))
                .ForCtorParam("ZipCode", opt => opt.MapFrom(src => src.ZipCode))
                .ForCtorParam("AddressType", opt => opt.MapFrom(src => src.AddressType));

            CreateMap<Address, AddressesWithIdDTO>()
                .ForCtorParam("AddressId", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("City", opt => opt.MapFrom(src => src.City))
                .ForCtorParam("District", opt => opt.MapFrom(src => src.District))
                .ForCtorParam("FullAddress", opt => opt.MapFrom(src => src.FullAddress))
                .ForCtorParam("ZipCode", opt => opt.MapFrom(src => src.ZipCode))
                .ForCtorParam("AddressType", opt => opt.MapFrom(src => src.AddressType));

            CreateMap<User, UserWithAddressesDTO>()
                .ForCtorParam("UserId", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("UserName", opt => opt.MapFrom(src => src.Username))
                .ForCtorParam("PhoneNumber", opt => opt.MapFrom(src => src.PhoneNumber))
                .ForCtorParam("EMail", opt => opt.MapFrom(src => src.Email))
                .ForCtorParam("Role", opt => opt.MapFrom(src => src.Role.RoleName))
                .ForCtorParam("Addresses", opt => opt.MapFrom(src => src.Addresses));

            CreateMap<User, UserDTO>()
                .ForCtorParam("Username", opt => opt.MapFrom(src => src.Username))
                .ForCtorParam("Email", opt => opt.MapFrom(src => src.Email))
                .ForCtorParam("PhoneNumber", opt => opt.MapFrom(src => src.PhoneNumber))
                .ForCtorParam("Role", opt => opt.MapFrom(src => src.Role.RoleName));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForCtorParam("OrderItemId", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("productId", opt => opt.MapFrom(src => src.ProductId))
                .ForCtorParam("ProductName", opt => opt.MapFrom(src => src.ProductName))
                .ForCtorParam("ImageUrl", opt => opt.MapFrom(src => src.ImageUrl))
                .ForCtorParam("Price", opt => opt.MapFrom(src => src.Price))
                .ForCtorParam("Quantity", opt => opt.MapFrom(src => src.Quantity));

            CreateMap<Order, OrderDTO>()
                .ForCtorParam("OrderId", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("UserId", opt => opt.MapFrom(src => src.UserId))
                .ForCtorParam("UserName", opt => opt.MapFrom(src => src.User.Username))
                .ForCtorParam("ShippingAddress", opt => opt.MapFrom(src => src.ShippingAddress))
                .ForCtorParam("TotalPrice", opt => opt.MapFrom(src => src.TotalPrice))
                .ForCtorParam("CreatedAt", opt => opt.MapFrom(src => src.CreatedAt))
                .ForCtorParam("OrderStatus", opt => opt.MapFrom(src => src.OrderStatus))
                .ForCtorParam("Items", opt => opt.MapFrom(src => src.Items));

            CreateMap<Review, GetReviewsResponseDTO>()
                .ForCtorParam("ReviewId", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("ProductId", opt => opt.MapFrom(src => src.ProductId))
                .ForCtorParam("ProductName", opt => opt.MapFrom(src => src.Product.ProductName))
                .ForCtorParam("UserId", opt => opt.MapFrom(src => src.UserId))
                .ForCtorParam("UserName", opt => opt.MapFrom(src => src.User.Username))
                .ForCtorParam("Rating", opt => opt.MapFrom(src => src.Rating))
                .ForCtorParam("Comment", opt => opt.MapFrom(src => src.Comment))
                .ForCtorParam("CreatedAt", opt => opt.MapFrom(src => src.CreatedAt));

        }

    }
}

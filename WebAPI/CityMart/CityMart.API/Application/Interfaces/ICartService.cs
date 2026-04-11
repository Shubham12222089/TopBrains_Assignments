using CityMart.API.DTOs;

namespace CityMart.API.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(string userId);
        Task<CartDto> AddToCartAsync(string userId, int productId, int quantity);
        Task<CartDto> UpdateCartItemAsync(string userId, int cartItemId, int quantity);
        Task<bool> RemoveFromCartAsync(string userId, int cartItemId);
        Task<bool> ClearCartAsync(string userId);
        Task<decimal> GetCartTotalAsync(string userId);
    }
}

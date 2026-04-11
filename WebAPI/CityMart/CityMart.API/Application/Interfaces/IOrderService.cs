using CityMart.API.DTOs;
using CityMart.API.Domain.Enums;

namespace CityMart.API.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string userId);
        Task<List<OrderDto>> GetUserOrdersAsync(string userId);
        Task<OrderDto> GetOrderByIdAsync(int orderId, string userId);
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<decimal> GetTotalRevenueAsync();
        Task<int> GetTotalOrdersAsync();
    }
}

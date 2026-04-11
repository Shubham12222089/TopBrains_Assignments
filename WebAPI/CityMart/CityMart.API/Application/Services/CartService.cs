using CityMart.API.Data;
using CityMart.API.DTOs;
using CityMart.API.Application.Interfaces;
using CityMart.API.Models;
using CityMart.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CityMart.API.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly ApplicationDbContext _context;

        public CartService(
            IRepository<Cart> cartRepository,
            IRepository<CartItem> cartItemRepository,
            IRepository<Product> productRepository,
            ApplicationDbContext context)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _context = context;
        }

        public async Task<CartDto> GetCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.AddAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }

            return MapToDto(cart);
        }

        public async Task<CartDto> AddToCartAsync(string userId, int productId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            if (product.Stock < quantity)
                throw new InvalidOperationException("Insufficient stock");

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.AddAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }

            var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };
                cart.Items.Add(cartItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            _cartRepository.Update(cart);
            await _cartRepository.SaveChangesAsync();

            return MapToDto(cart);
        }

        public async Task<CartDto> UpdateCartItemAsync(string userId, int cartItemId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new KeyNotFoundException("Cart not found");

            var cartItem = cart.Items.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null)
                throw new KeyNotFoundException("Cart item not found");

            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
            if (product.Stock < quantity)
                throw new InvalidOperationException("Insufficient stock");

            cartItem.Quantity = quantity;
            cart.UpdatedAt = DateTime.UtcNow;
            _cartRepository.Update(cart);
            await _cartRepository.SaveChangesAsync();

            return MapToDto(cart);
        }

        public async Task<bool> RemoveFromCartAsync(string userId, int cartItemId)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.Cart.UserId == userId);

            if (cartItem == null)
                return false;

            _cartItemRepository.Remove(cartItem);
            await _cartItemRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return false;

            _cartItemRepository.RemoveRange(cart.Items);
            await _cartItemRepository.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetCartTotalAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return 0;

            return cart.Items.Sum(ci => ci.Price * ci.Quantity);
        }

        private CartDto MapToDto(Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = cart.Items.Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product?.Name,
                    Quantity = ci.Quantity,
                    Price = ci.Price,
                    TotalPrice = ci.Price * ci.Quantity
                }).ToList(),
                TotalPrice = cart.Items.Sum(ci => ci.Price * ci.Quantity),
                CreatedAt = cart.CreatedAt,
                UpdatedAt = cart.UpdatedAt
            };
        }
    }
}

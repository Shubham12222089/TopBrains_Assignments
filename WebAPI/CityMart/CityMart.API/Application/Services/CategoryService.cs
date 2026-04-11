using CityMart.API.Data;
using CityMart.API.DTOs;
using CityMart.API.Application.Interfaces;
using CityMart.API.Models;
using CityMart.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CityMart.API.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly ApplicationDbContext _context;

        public CategoryService(IRepository<Category> categoryRepository, ApplicationDbContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {id} not found");

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name));

            var category = new Category
            {
                Name = dto.Name.Trim(),
                Description = dto.Description?.Trim() ?? ""
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {id} not found");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                category.Name = dto.Name.Trim();

            if (!string.IsNullOrWhiteSpace(dto.Description))
                category.Description = dto.Description.Trim();

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return false;

            _categoryRepository.Remove(category);
            await _categoryRepository.SaveChangesAsync();
            return true;
        }
    }
}

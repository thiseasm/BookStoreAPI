using BookStore.Core.Abstractions.Models;
using BookStore.Infrastructure.DTOs;

namespace BookStore.Core.Extensions
{
    public static class CategoryDtoExtensions
    {
        public static Category ToDomain(this CategoryDto dto)
        {
            return new Category
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}

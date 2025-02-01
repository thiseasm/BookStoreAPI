using BookStore.Core.Abstractions.Models;
using BookStore.Infrastructure.Abstractions.DTOs;

namespace BookStore.Core.Extensions
{
    public static class BookDtoExtensions
    {
        public static Book ToDomain(this BookDto dto)
        {
            return new Book
            {
                Id = dto.Id,
                Name = dto.Name,
                Category = new Category
                {
                    Id = dto.Category.Id,
                    Name = dto.Category.Name
                }
            };
        }
    }
}

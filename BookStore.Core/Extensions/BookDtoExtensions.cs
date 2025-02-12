﻿using BookStore.Core.Abstractions.Models.Books;
using BookStore.Core.Abstractions.Models.Categories;
using BookStore.Infrastructure.DTOs;

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

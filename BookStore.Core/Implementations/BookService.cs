﻿using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Extensions;
using BookStore.Data.Abstractions.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Implementations
{
    public class BookService(BookStoreContext dbContext) : IBookService
    {
        public async Task<IList<Book>> GetBooksAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Books
                .Include(book => book.Category)
                .AsNoTracking()
                .Select(book => book.ToDomain())
                .ToListAsync(cancellationToken);
        }
    }
}

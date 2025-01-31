using BookStore.Data.Abstractions.DTOs;
using BookStore.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Abstractions.Context
{
    public class BookStoreContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDto>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRoleDto>();

            modelBuilder.Entity<BookDto>()
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}

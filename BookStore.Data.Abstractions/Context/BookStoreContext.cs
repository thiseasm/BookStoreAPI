using BookStore.Data.Abstractions.DTOs;
using BookStore.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Abstractions.Context
{
    public class BookStoreContext(DbContextOptions<BookStoreContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDto>()
                .ToTable("User")
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRoleDto>();

            modelBuilder.Entity<RoleDto>()
                .ToTable("Role");

            modelBuilder.Entity<BookDto>()
                .ToTable("Book")
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .IsRequired();

            modelBuilder.Entity<CategoryDto>()
                .ToTable("Category");

            modelBuilder.Entity<UserRoleDto>()
                .ToTable("UserRoles");

            base.OnModelCreating(modelBuilder);
        }
    }
}

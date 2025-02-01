using BookStore.Infrastructure.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data
{
    public class BookStoreContext(DbContextOptions<BookStoreContext> options) : DbContext(options)
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<RoleDto> Roles { get; set; }
        public DbSet<BookDto> Books { get; set; }
        public DbSet<CategoryDto> Categories { get; set; }

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

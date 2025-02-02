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
        public DbSet<UserLogDto> UserLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDto>()
                .ToTable("User")
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRoleDto>();

            modelBuilder.Entity<RoleDto>()
                .ToTable("Role")
                .HasData(
                    new RoleDto { Id = 1, Name = "Admin" },
                    new RoleDto { Id = 2, Name = "IT" },
                    new RoleDto { Id = 3, Name = "Moderator" },
                    new RoleDto { Id = 4, Name = "Test" },
                    new RoleDto { Id = 5, Name = "User" }
                );

            modelBuilder.Entity<BookDto>()
                .ToTable("Book")
                .HasOne(b => b.Category)
                .WithMany()
                .HasForeignKey(b => b.CategoryId)
                .IsRequired();

            modelBuilder.Entity<CategoryDto>()
                .ToTable("Category")
                .HasData(
                    new CategoryDto { Id = 1, Name = "Fiction" },
                    new CategoryDto { Id = 2, Name = "Science" },
                    new CategoryDto { Id = 3, Name = "Horror" },
                    new CategoryDto { Id = 4, Name = "Mystery" },
                    new CategoryDto { Id = 5, Name = "Romance" },
                    new CategoryDto { Id = 6, Name = "Biography" },
                    new CategoryDto { Id = 7, Name = "Poetry" },
                    new CategoryDto { Id = 8, Name = "Fantasy" }
                );                

            modelBuilder.Entity<UserRoleDto>()
                .ToTable("UserRoles");

            modelBuilder.Entity<UserLogDto>()
                .ToTable("UserLogs")
                .HasKey(l => new {l.Timestamp, l.EntityId });

            base.OnModelCreating(modelBuilder);
        }
    }
}

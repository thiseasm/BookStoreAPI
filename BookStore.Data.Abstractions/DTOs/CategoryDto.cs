using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.Abstractions.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }
    }
}

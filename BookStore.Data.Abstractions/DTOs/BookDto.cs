using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }
        public required int CategoryId { get; set; }
        public required CategoryDto Category { get; set; }
    }
}

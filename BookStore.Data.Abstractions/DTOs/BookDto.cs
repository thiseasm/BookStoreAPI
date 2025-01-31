using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Abstractions.Models
{
    public class BookDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}

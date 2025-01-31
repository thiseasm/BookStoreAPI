using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Abstractions.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}

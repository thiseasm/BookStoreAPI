using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Abstractions.Models.Books
{
    public class CreateBookRequest
    {
        [StringLength(100)]
        public required string Name { get; set; }

        public required int CategoryId { get; set; }
    }
}

using BookStore.Core.Abstractions.Models.Categories;

namespace BookStore.Core.Abstractions.Models.Books
{
    public class Book
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required Category Category { get; set; }
    }
}

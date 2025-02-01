namespace BookStore.Core.Abstractions.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Category Category { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Abstractions.Models.Users
{
    public class CreateUserRequest
    {
        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(100)]
        public required string Surname { get; set; }
    }
}

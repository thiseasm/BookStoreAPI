using BookStore.Core.Abstractions.Models.Roles;

namespace BookStore.Core.Abstractions.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public List<Role> Roles { get; set; } = [];
    }
}

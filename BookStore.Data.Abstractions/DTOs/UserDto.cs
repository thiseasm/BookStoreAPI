namespace BookStore.Data.Abstractions.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<RoleDto> Roles { get; } = [];
    }
}

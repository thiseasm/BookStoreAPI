namespace BookStore.Data.Abstractions.Models
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserDto> Users { get; } = [];
    }
}

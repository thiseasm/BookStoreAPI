using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }

        public List<UserDto> Users { get; } = [];
    }
}

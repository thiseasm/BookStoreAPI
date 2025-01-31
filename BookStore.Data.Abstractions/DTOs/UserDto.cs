using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Abstractions.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        public List<RoleDto> Roles { get; } = [];
    }
}

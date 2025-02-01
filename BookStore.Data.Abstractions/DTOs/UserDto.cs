using BookStore.Data.Abstractions.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.Abstractions.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(100)]
        public required string Surname { get; set; }

        public List<RoleDto> Roles { get; } = [];
    }
}

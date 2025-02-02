using BookStore.Core.Abstractions.Models.Roles;
using BookStore.Core.Abstractions.Models.Users;
using BookStore.Infrastructure.DTOs;

namespace BookStore.Core.Extensions
{
    public static class UserDtoExtensions
    {
        public static User ToDomain(this UserDto request)
        {
            var roles = request.Roles.Select(r => new Role
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return new User
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Roles = roles
            };
        }
    }
}

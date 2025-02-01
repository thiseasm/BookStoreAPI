using BookStore.Core.Abstractions.Models.Users;
using BookStore.Infrastructure.Abstractions.DTOs;

namespace BookStore.Core.Extensions
{
    public static class CreateUserRequestExtensions
    {
        public static UserDto ToDto(this CreateUserRequest request)
        {
            return new UserDto
            {
                Name = request.Name,
                Surname = request.Surname
            };
        }
    }
}

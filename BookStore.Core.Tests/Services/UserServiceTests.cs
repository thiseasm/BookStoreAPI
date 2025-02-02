using BookStore.Core.Abstractions.Events;
using BookStore.Core.Abstractions.Models.Users;
using BookStore.Core.Services;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookStore.Core.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;

        public UserServiceTests()
        {
            _loggerMock = new Mock<ILogger<UserService>>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task UpdateUserRolesAsync_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var request = new UpdateUserRolesRequest { RoleIds = [] };
            var sut = new UserService(_loggerMock.Object, _mediatorMock.Object, GetNewDbContext());

            // Act
            var result = await sut.UpdateUserRolesAsync(1, request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
            Assert.Null(result.Data);
            Assert.Equal("User with ID: 1 not found", result.Error);

            _mediatorMock.Verify(m => m.Send(It.IsAny<UserRolesUpdatedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateUserRolesAsync_ClearRoles_ReturnsOk()
        {
            // Arrange
            using var dbContext = GetNewDbContext();
            var user = new UserDto { Id = 1, Name = "John", Surname = "Doe", Roles = [new RoleDto { Id = 1, Name = "Admin" }] };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var sut = new UserService(_loggerMock.Object, _mediatorMock.Object, dbContext);
            var request = new UpdateUserRolesRequest { RoleIds = [] };

            // Act
            var result = await sut.UpdateUserRolesAsync(1, request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal("Roles for User with ID: 1 have been cleared", result.Data);
            Assert.Empty(user.Roles);

            _mediatorMock.Verify(m => m.Send(It.IsAny<UserRolesUpdatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUserRolesAsync_RolesUpdatedSuccessfully_ReturnsOk()
        {
            // Arrange
            using var dbContext = GetNewDbContext();
            var user = new UserDto { Id = 1, Name = "John", Surname = "Doe", Roles = [] };
            var role1 = new RoleDto { Id = 1, Name = "Admin" };
            var role2 = new RoleDto { Id = 2, Name = "User" };
            dbContext.Users.Add(user);
            dbContext.Roles.AddRange(role1, role2);
            await dbContext.SaveChangesAsync();

            var sut = new UserService(_loggerMock.Object, _mediatorMock.Object, dbContext);
            var request = new UpdateUserRolesRequest { RoleIds = [1, 2] };

            // Act
            var result = await sut.UpdateUserRolesAsync(1, request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Null(result.Error);
            Assert.Equal(200, result.Code);
            Assert.Equal("Roles for User with ID: 1 have been updated", result.Data);
            Assert.Equal(2, user.Roles.Count);
            Assert.Contains(user.Roles, r => r.Id == 1);
            Assert.Contains(user.Roles, r => r.Id == 2);

            _mediatorMock.Verify(m => m.Send(It.IsAny<UserRolesUpdatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);

        }

        private static BookStoreContext GetNewDbContext() =>
            new(new DbContextOptionsBuilder<BookStoreContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
    }
}



using Xunit;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Tests.Features.Auth
{
    public class UserEntityTests
    {
        [Fact]
        public void CreateUser_WithValidData_ShouldCreateSuccessfully()
        {
            // Arrange
            var email = "test@example.com";
            var passwordHash = "hashedpassword";
            var fullName = "Test User";

            // Act
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = passwordHash,
                FullName = fullName,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
            Assert.Equal(passwordHash, user.PasswordHash);
            Assert.Equal(fullName, user.FullName);
        }

        [Fact]
        public void User_ShouldHaveUniqueId()
        {
            // Arrange & Act
            var user1 = new User
            {
                Id = Guid.NewGuid(),
                Email = "user1@example.com",
                PasswordHash = "hash1",
                FullName = "User One",
                CreatedAt = DateTime.UtcNow
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                Email = "user2@example.com",
                PasswordHash = "hash2",
                FullName = "User Two",
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.NotEqual(user1.Id, user2.Id);
        }

        [Fact]
        public void User_ShouldStoreCreatedAtTimestamp()
        {
            // Arrange
            var createdAt = DateTime.UtcNow;

            // Act
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                PasswordHash = "hash",
                FullName = "Test User",
                CreatedAt = createdAt
            };

            // Assert
            Assert.Equal(createdAt, user.CreatedAt);
        }

        [Theory]
        [InlineData("test@example.com")]
        [InlineData("user@domain.co.uk")]
        [InlineData("admin@company.org")]
        public void User_ShouldAcceptValidEmails(string email)
        {
            // Arrange & Act
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = "hash",
                FullName = "Test User",
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(email, user.Email);
        }
    }
}

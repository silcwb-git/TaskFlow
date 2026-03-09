using Xunit;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Tests.Infrastructure
{
    public class GenericRepositoryTests
    {
        private readonly DbContextOptions<TaskFlowDbContext> _dbContextOptions;

        public GenericRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<TaskFlowDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async System.Threading.Tasks.Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            using var context = new TaskFlowDbContext(_dbContextOptions);
            var repository = new GenericRepository<User>(context);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                FullName = "Test User",
                CreatedAt = DateTime.UtcNow
            };

            // Act
            await repository.AddAsync(user);
            await repository.SaveChangesAsync();

            // Assert
            var addedUser = await repository.GetByIdAsync(user.Id);
            Assert.NotNull(addedUser);
            Assert.Equal(user.Email, addedUser.Email);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetByIdAsync_ShouldReturnEntity()
        {
            // Arrange
            using var context = new TaskFlowDbContext(_dbContextOptions);
            var repository = new GenericRepository<User>(context);
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                FullName = "Test User",
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();

            // Act
            var retrievedUser = await repository.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(userId, retrievedUser.Id);
            Assert.Equal("test@example.com", retrievedUser.Email);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetByIdAsync_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            using var context = new TaskFlowDbContext(_dbContextOptions);
            var repository = new GenericRepository<User>(context);
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await repository.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            using var context = new TaskFlowDbContext(_dbContextOptions);
            var repository = new GenericRepository<User>(context);
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Email = "user1@example.com", PasswordHash = "hash1", FullName = "User One", CreatedAt = DateTime.UtcNow },
                new User { Id = Guid.NewGuid(), Email = "user2@example.com", PasswordHash = "hash2", FullName = "User Two", CreatedAt = DateTime.UtcNow },
                new User { Id = Guid.NewGuid(), Email = "user3@example.com", PasswordHash = "hash3", FullName = "User Three", CreatedAt = DateTime.UtcNow }
            };

            foreach (var user in users)
            {
                await repository.AddAsync(user);
            }
            await repository.SaveChangesAsync();

            // Act
            var allUsers = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(allUsers);
            Assert.Equal(3, allUsers.Count());
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateAsync_ShouldUpdateEntity()
        {
            // Arrange
            using var context = new TaskFlowDbContext(_dbContextOptions);
            var repository = new GenericRepository<User>(context);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                FullName = "Test User",
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();

            // Act
            user.Email = "updated@example.com";
            await repository.UpdateAsync(user);
            await repository.SaveChangesAsync();

            // Assert
            var updatedUser = await repository.GetByIdAsync(user.Id);
            Assert.NotNull(updatedUser);
            Assert.Equal("updated@example.com", updatedUser.Email);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteAsync_ShouldRemoveEntity()
        {
            // Arrange
            using var context = new TaskFlowDbContext(_dbContextOptions);
            var repository = new GenericRepository<User>(context);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                FullName = "Test User",
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();

            // Act
            await repository.DeleteAsync(user);
            await repository.SaveChangesAsync();

            // Assert
            var deletedUser = await repository.GetByIdAsync(user.Id);
            Assert.Null(deletedUser);
        }
    }
}

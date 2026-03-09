using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using TaskFlow.Infrastructure.Security;
using System.Security.Claims;

namespace TaskFlow.Tests.Features.Auth
{
    public class JwtTokenServiceTests
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly Mock<IConfiguration> _configurationMock;

        public JwtTokenServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            
            // Setup configuration
            _configurationMock.Setup(x => x["Jwt:SecretKey"])
                .Returns("my-super-secret-key-that-is-very-long-and-secure");
            _configurationMock.Setup(x => x["Jwt:Issuer"])
                .Returns("TaskFlow");
            _configurationMock.Setup(x => x["Jwt:Audience"])
                .Returns("TaskFlowUsers");
            _configurationMock.Setup(x => x["Jwt:ExpirationMinutes"])
                .Returns("60");

            _jwtTokenService = new JwtTokenService(_configurationMock.Object);
        }

        [Fact]
        public void GenerateAccessToken_ShouldReturnValidToken()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var email = "test@example.com";

            // Act
            var token = _jwtTokenService.GenerateAccessToken(userId, email);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
            Assert.IsType<string>(token);
        }

        [Fact]
        public void GenerateAccessToken_TokenShouldContainUserIdClaim()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var email = "test@example.com";

            // Act
            var token = _jwtTokenService.GenerateAccessToken(userId, email);
            var principal = _jwtTokenService.ValidateToken(token);

            // Assert
            Assert.NotNull(principal);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            Assert.NotNull(userIdClaim);
            Assert.Equal(userId.ToString(), userIdClaim.Value);
        }

        [Fact]
        public void GenerateAccessToken_TokenShouldContainEmailClaim()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var email = "test@example.com";

            // Act
            var token = _jwtTokenService.GenerateAccessToken(userId, email);
            var principal = _jwtTokenService.ValidateToken(token);

            // Assert
            Assert.NotNull(principal);
            var emailClaim = principal.FindFirst(ClaimTypes.Email);
            Assert.NotNull(emailClaim);
            Assert.Equal(email, emailClaim.Value);
        }

        [Fact]
        public void GenerateRefreshToken_ShouldReturnValidToken()
        {
            // Act
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            // Assert
            Assert.NotNull(refreshToken);
            Assert.NotEmpty(refreshToken);
            Assert.IsType<string>(refreshToken);
        }

        [Fact]
        public void ValidateToken_WithValidToken_ShouldReturnPrincipal()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var email = "test@example.com";
            var token = _jwtTokenService.GenerateAccessToken(userId, email);

            // Act
            var principal = _jwtTokenService.ValidateToken(token);

            // Assert
            Assert.NotNull(principal);
        }

        [Fact]
        public void ValidateToken_WithInvalidToken_ShouldReturnNull()
        {
            // Arrange
            var invalidToken = "invalid.token.here";

            // Act
            var principal = _jwtTokenService.ValidateToken(invalidToken);

            // Assert
            Assert.Null(principal);
        }

        [Fact]
        public void ValidateToken_WithEmptyToken_ShouldReturnNull()
        {
            // Arrange
            var emptyToken = string.Empty;

            // Act
            var principal = _jwtTokenService.ValidateToken(emptyToken);

            // Assert
            Assert.Null(principal);
        }
    }
}

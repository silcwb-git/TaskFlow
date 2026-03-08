using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Infrastructure.Security;
using System.Security.Cryptography;
using System.Text;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IGenericRepository<User> userRepository,
            IJwtTokenService jwtTokenService,
            ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto dto)
        {
            _logger.LogInformation("Login attempt for email: {Email}", dto.Email);

            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
            {
                _logger.LogWarning("Failed login for email: {Email}", dto.Email);
                return Unauthorized("Invalid credentials");
            }

            var accessToken = _jwtTokenService.GenerateAccessToken(user.Id, user.Email);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            return Ok(new LoginResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDto>> Register([FromBody] LoginDto dto)
        {
            _logger.LogInformation("Register attempt for email: {Email}", dto.Email);

            var users = await _userRepository.GetAllAsync();
            if (users.Any(u => u.Email == dto.Email))
            {
                return BadRequest("Email already registered");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                FullName = dto.Email.Split('@')[0],
                PasswordHash = HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var accessToken = _jwtTokenService.GenerateAccessToken(user.Id, user.Email);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            return CreatedAtAction(nameof(Login), new LoginResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }
    }
}
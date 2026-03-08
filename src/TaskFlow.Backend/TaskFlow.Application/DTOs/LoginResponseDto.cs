namespace TaskFlow.Application.DTOs
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
namespace TaskFlow.Application.DTOs
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}

namespace ARMauiApp.Models
{
    public class UserRegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public string Password { get; set; } = string.Empty;
        public int Gender { get; set; }
        public string RoleId { get; set; } = string.Empty;
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}

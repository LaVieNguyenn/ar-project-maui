namespace ARMauiApp.Models
{
    public class UserUpdateDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public int Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}

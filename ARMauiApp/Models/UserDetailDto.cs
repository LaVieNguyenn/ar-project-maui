namespace ARMauiApp.Models
{
    public class UserDetailDto
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public int Gender { get; set; }
        public string? RoleId { get; set; }
        public bool IsActive { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

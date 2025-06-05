namespace ARProject.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int Gender { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime updatedAt { get; set; }
    }
}

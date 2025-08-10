namespace ARMauiApp.Models
{
    public partial class CategoryDto : ObservableObject
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string? ParentCategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ObservableProperty]
        private bool isSelected;
    }
}

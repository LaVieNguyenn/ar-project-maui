namespace ARMauiApp.Models
{
    public class PromotionDto
    {
        public string? ImageUrl { get; set; }
        public string? DiscountText { get; set; }
        public string? Headline { get; set; }
        public string? Subheadline { get; set; }
        public string? ButtonText { get; set; }
        // Optional: filter or navigation targets
        public string? TargetCategoryId { get; set; }
        public string? NavigateTo { get; set; }
    }
}

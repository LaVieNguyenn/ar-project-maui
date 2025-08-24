using ARMauiApp.Services;

namespace ARMauiApp.Pages;

public partial class OnboardingPage : ContentPage
{
    private readonly IOnboardingService _onboardingService;

    private class OnboardingItem
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
    }

    private readonly IList<OnboardingItem> _items = new List<OnboardingItem>
    {
        new() {
            Title = "Chọn sản phẩm",
            Description = "Đa dạng phong cách – Thể hiện cá tính của bạn!\nTừ quần áo, phụ kiện đến giày dép, mọi thứ đã sẵn sàng để bạn phối theo cách riêng.",
            Image = "onboarding_1"
        },
        new() {
            Title = "Công nghệ AR",
            Description = "Tìm kích cỡ hoàn hảo mà không cần thử!\nCông nghệ AR giúp bạn thử sản phẩm ảo, đảm bảo đúng kích cỡ và phong cách trước khi mua.",
            Image = "onboarding_2"
        },
        new() {
            Title = "So sánh giá",
            Description = "So sánh giá, săn ưu đãi tốt nhất!\nTìm sản phẩm yêu thích với mức giá tốt nhất trên nhiều nền tảng.",
            Image = "onboarding_3"
        },
        new() {
            Title = "Bắt đầu",
            Description = "Hãy đăng nhập để tiếp tục trải nghiệm.",
            Image = "onboarding_4"
        },
    };


    public OnboardingPage(IOnboardingService onboardingService)
    {
        InitializeComponent();
        _onboardingService = onboardingService;

        Carousel.ItemsSource = _items;
        Indicators.Count = _items.Count;
        Carousel.PositionChanged += Carousel_PositionChanged;
        UpdateUi(0);
    }

    private void Carousel_PositionChanged(object? sender, PositionChangedEventArgs e)
    {
        UpdateUi(e.CurrentPosition);
    }

    private void UpdateUi(int position)
    {
        var lastIndex = _items.Count - 1;
        var isLast = position >= lastIndex;
        NextButton.Text = isLast ? "Bắt đầu" : "Tiếp";
        ProgressLabel.Text = $"{position + 1}/{_items.Count}";
    }

    private async void OnSkipClicked(object sender, EventArgs e)
    {
        _onboardingService.CompleteOnboarding();
        await Shell.Current.GoToAsync("//login");
    }

    private async void OnNextClicked(object sender, EventArgs e)
    {
        var lastIndex = _items.Count - 1;
        var isLast = Carousel.Position >= lastIndex;
        if (isLast)
        {
            _onboardingService.CompleteOnboarding();
            await Shell.Current.GoToAsync("//login");
        }
        else
        {
            Carousel.Position += 1;
        }
    }
}

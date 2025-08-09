using ARMauiApp.Helpers;
using ARMauiApp.Services;

namespace ARMauiApp.Pages;

public partial class SplashPage : ContentPage
{
    private readonly IOnboardingService _onboardingService;
    public SplashPage(IOnboardingService onboardingService)
    {
        InitializeComponent();
        _onboardingService = onboardingService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CheckAuthenticationAndNavigate();
    }

    private async Task CheckAuthenticationAndNavigate()
    {
        try
        {
            // Đợi một chút để splash screen hiển thị
            await Task.Delay(1000);

            // Nếu lần chạy đầu tiên -> vào onboarding
            if (_onboardingService.IsFirstRun())
            {
                await Shell.Current.GoToAsync("//onboarding");
                return;
            }

            var authService = ServiceHelper.GetService<AuthService>();

            if (authService != null)
            {
                var (isAuthenticated, user) = await authService.CheckAuthenticationAsync();

                if (isAuthenticated && user != null)
                {
                    // User đã đăng nhập và token hợp lệ, đi đến trang chính
                    await Shell.Current.GoToAsync("//products");
                }
                else
                {
                    // User chưa đăng nhập hoặc token không hợp lệ, đi đến trang login
                    await Shell.Current.GoToAsync("//login");
                }
            }
            else
            {
                // Không thể lấy AuthService, đi đến trang login
                await Shell.Current.GoToAsync("//login");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error checking authentication: {ex.Message}");
            // Nếu có lỗi, đi đến trang login
            await Shell.Current.GoToAsync("//login");
        }
    }
}

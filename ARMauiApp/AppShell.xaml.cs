using ARMauiApp.Pages;
using ARMauiApp.Services;

namespace ARMauiApp;

public partial class AppShell : Shell
{
    private readonly CartService _cart;

#if ANDROID
    private readonly ITabBadgeService _badgeSvc;
    private const int CartTabIndex = 1;
#endif

    // 👇 Constructor điều kiện theo nền tảng
    public AppShell(
        CartService cart
#if ANDROID
        , ITabBadgeService badgeSvc
#endif
    )
    {
        InitializeComponent();
        _cart = cart;

#if ANDROID
        _badgeSvc = badgeSvc;

        HandlerChanged += (_, __) => ApplyBadge(_cart.LastCount);
        Loaded +=       (_, __) => ApplyBadge(_cart.LastCount);
        Navigated +=    (_, __) => ApplyBadge(_cart.LastCount);
        PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(CurrentItem))
                ApplyBadge(_cart.LastCount);
        };

        // Lắng nghe thay đổi số lượng giỏ
        _cart.CountChanged += (_, count) => ApplyBadge(count);

        // Refresh lần đầu (có thể gọi API) rồi cập nhật badge
        _ = Task.Run(async () =>
        {
            var count = await _cart.RefreshCountAsync();
            ApplyBadge(count);
        });

        // Deep link từ push/intents: Intent extra "navigate_to"
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var route = activity?.Intent?.GetStringExtra("navigate_to");
            if (!string.IsNullOrEmpty(route))
            {
                GoToAsync("//" + route);
            }
        });
#endif

        // Đăng ký routes
        Routing.RegisterRoute("productdetail", typeof(ProductDetailPage));
        Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
        Routing.RegisterRoute("editprofile", typeof(EditProfilePage));
        Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
        Routing.RegisterRoute(nameof(PlanQrPopup), typeof(PlanQrPopup));
    }

#if ANDROID
    private void ApplyBadge(int count)
    {
        if (Handler is null) return;
        Dispatcher.Dispatch(() => _badgeSvc.SetBadge(this, CartTabIndex, count));
    }
#endif
}

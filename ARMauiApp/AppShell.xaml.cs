using ARMauiApp.Pages;
using ARMauiApp.Services;

namespace ARMauiApp;

public partial class AppShell : Shell
{
    readonly ITabBadgeService _badgeSvc;
    readonly CartService _cart;
    const int CartTabIndex = 1;
    public AppShell(ITabBadgeService badgeSvc, CartService cart)
    {
        InitializeComponent();
        _badgeSvc = badgeSvc;
        _cart = cart;

#if ANDROID
        HandlerChanged += (_, __) => ApplyBadge(_cart.LastCount);
        Loaded += (_, __) => ApplyBadge(_cart.LastCount);
        Navigated += (_, __) => ApplyBadge(_cart.LastCount);
        PropertyChanged += (_, e) => { if (e.PropertyName == nameof(CurrentItem)) ApplyBadge(_cart.LastCount); };
#endif
        _cart.CountChanged += (_, count) => ApplyBadge(count);

        _ = Task.Run(async () =>
        {
            var count = await _cart.RefreshCountAsync();
            ApplyBadge(count);
        });

#if ANDROID
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


        // Register routes for navigation
        Routing.RegisterRoute("productdetail", typeof(ProductDetailPage));
        Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
        Routing.RegisterRoute("editprofile", typeof(EditProfilePage));
        Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
        Routing.RegisterRoute(nameof(PlanQrPopup), typeof(PlanQrPopup));
    }

#if ANDROID
    void ApplyBadge(int count)
    {
        if (Handler is null) return;
        Dispatcher.Dispatch(() => _badgeSvc.SetBadge(this, CartTabIndex, count));
    }
#endif
}

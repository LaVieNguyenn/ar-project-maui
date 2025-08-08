using ARMauiApp.Pages;

namespace ARMauiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

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
}

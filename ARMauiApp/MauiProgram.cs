using ARMauiApp.Converters;
using ARMauiApp.Pages;
using ARMauiApp.Services;
using ARMauiApp.Unity;
using ARMauiApp.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Maui.Handlers;
using UraniumUI;
using ZXing.Net.Maui.Controls;
using Microsoft.Maui.LifecycleEvents;

#if IOS
using UIKit;
using Foundation;
#endif
namespace ARMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseBarcodeReader()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddMaterialSymbolsFonts();
            });
#if IOS
        PickerHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
        {
            var tf = handler.PlatformView; // UITextField
            tf.BorderStyle = UITextBorderStyle.None;
            tf.BackgroundColor = UIColor.Clear;
            tf.Layer.CornerRadius = 0;
            tf.ClipsToBounds = false;
        });

        DatePickerHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
        {
            var tf = handler.PlatformView;
            tf.BorderStyle = UITextBorderStyle.None;
            tf.BackgroundColor = UIColor.Clear;
            tf.Layer.CornerRadius = 0;
            tf.ClipsToBounds = false;
        });
#endif
        builder.ConfigureLifecycleEvents(events =>
        {
#if __IOS__
    events.AddiOS(ios =>
    {
        ios.FinishedLaunching((app, dict) =>
        {
            // Luôn Light
            UIWindow? keyWindow = null;
            if (OperatingSystem.IsIOSVersionAtLeast(13))
            {
                keyWindow = UIApplication.SharedApplication
                    .ConnectedScenes.OfType<UIWindowScene>()
                    .SelectMany(s => s.Windows)
                    .FirstOrDefault(w => w.IsKeyWindow);
            }
            else
            {
                keyWindow = UIApplication.SharedApplication.KeyWindow;
            }
            if (keyWindow != null)
                keyWindow.OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;

            // NavBar
            var nav = new UINavigationBarAppearance();
            nav.ConfigureWithOpaqueBackground();
            nav.BackgroundColor = UIColor.White;
            nav.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.Black };
            UINavigationBar.Appearance.StandardAppearance = nav;
            UINavigationBar.Appearance.ScrollEdgeAppearance = nav;
            UINavigationBar.Appearance.TintColor = UIColor.Black; // back button / icons

            // TabBar (iOS 15+: cần Standard + ScrollEdge)
            var tab = new UITabBarAppearance();
            tab.ConfigureWithOpaqueBackground();
            tab.BackgroundColor = UIColor.White;
            tab.StackedLayoutAppearance.Selected.IconColor = UIColor.Black;
            tab.StackedLayoutAppearance.Selected.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.Black };
            tab.StackedLayoutAppearance.Normal.IconColor = UIColor.FromWhiteAlpha(0f, 0.4f); // #66000000
            tab.StackedLayoutAppearance.Normal.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.FromWhiteAlpha(0f, 0.4f) };
            UITabBar.Appearance.StandardAppearance = tab;
            UITabBar.Appearance.ScrollEdgeAppearance = tab;
            UITabBar.Appearance.TintColor = UIColor.Black;
            UITabBar.Appearance.UnselectedItemTintColor = UIColor.FromWhiteAlpha(0f, 0.4f);

            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.DarkContent;
            return true;
        });

        ios.WillEnterForeground(app =>
        {
            // apply lại Light khi quay lại app
            UIWindow? win = UIApplication.SharedApplication
                .ConnectedScenes.OfType<UIWindowScene>()
                .SelectMany(s => s.Windows)
                .FirstOrDefault(w => w.IsKeyWindow);
            if (win != null)
                win.OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
        });
    });
#endif
        });

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
            if (view is Entry)
            {
#if ANDROID
                // Remove underline
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            }
        });

        builder.Services.AddSingleton<AppShell>();


        // Register Services
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<ProductService>();
        builder.Services.AddSingleton<CategoryService>();
        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddSingleton<CartService>();
        builder.Services.AddSingleton<PlanService>();
        builder.Services.AddSingleton<PlanPaymentService>();
#if ANDROID
        builder.Services.AddSingleton<ITabBadgeService, TabBadgeService>();
#endif
        builder.Services.AddSingleton<IOnboardingService, OnboardingService>();



        // Register Converters
        builder.Services.AddSingleton<CountToBoolConverter>();
        builder.Services.AddSingleton<InvertedBoolConverter>();
        builder.Services.AddSingleton<BoolToActiveStatusConverter>();
        builder.Services.AddSingleton<BoolToStatusColorConverter>();
        builder.Services.AddSingleton<GreaterThanZeroConverter>();
        builder.Services.AddSingleton<EqualsMultiConverter>();
        builder.Services.AddSingleton<BoolNegateConverter>();
        builder.Services.AddSingleton<DiscountPriceConverter>();
        builder.Services.AddSingleton<BrushResourceConverter>();

        // Register ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<ProductListViewModel>();
        builder.Services.AddTransient<ProductDetailViewModel>();
        builder.Services.AddTransient<AccountViewModel>();
        builder.Services.AddTransient<CartViewModel>();
        builder.Services.AddTransient<PlanListViewModel>();
        builder.Services.AddTransient<PlanQrPopupViewModel>();


        // Register Pages
        builder.Services.AddTransient<SplashPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<ProductListPage>();
        builder.Services.AddTransient<ProductDetailPage>();
        builder.Services.AddTransient<AccountPage>();
        builder.Services.AddTransient<EditProfilePage>();
        builder.Services.AddTransient<CartPage>();
        builder.Services.AddTransient<LoadingPage>();
        builder.Services.AddTransient<PlansPage>();
        builder.Services.AddTransient<PlanQrPopup>();
        builder.Services.AddTransient<OnboardingPage>();

        UnityBridge.Init();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

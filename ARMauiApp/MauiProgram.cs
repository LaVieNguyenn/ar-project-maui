using ARMauiApp.Converters;
using ARMauiApp.Pages;
using ARMauiApp.Platforms.Android;
using ARMauiApp.Services;
using ARMauiApp.Unity;
using ARMauiApp.ViewModels;
using CommunityToolkit.Maui;
using UraniumUI;
using ZXing.Net.Maui.Controls;

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
        builder.Services.AddSingleton<ITabBadgeService, TabBadgeService>();



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

        UnityBridge.Init();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

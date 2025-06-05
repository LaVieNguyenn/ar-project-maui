using ARProject.Views;
using Fonts;

namespace ARProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            HomeTab.Icon = CreateIcon(FluentUI.home_24_regular);
            WishlistTab.Icon = CreateIcon(FluentUI.heart_24_regular);
            ARTab.Icon = CreateIcon(FluentUI.camera_24_regular);
            PaymentTab.Icon = CreateIcon(FluentUI.wallet_credit_card_24_regular);
            SettingTab.Icon = CreateIcon(FluentUI.settings_24_regular);

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(WishlistPage), typeof(WishlistPage));
            Routing.RegisterRoute(nameof(ARPage), typeof(ARPage));
            Routing.RegisterRoute(nameof(PaymentPage), typeof(PaymentPage));
            Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
        }

        private FontImageSource CreateIcon(string glyph)
        {
            return new FontImageSource
            {
                FontFamily = FluentUI.FontFamily,
                Glyph = glyph,
                Size = 24,
                Color = Colors.Black
            };
        }
    }
}

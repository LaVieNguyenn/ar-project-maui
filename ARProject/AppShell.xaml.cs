using ARProject.Views;
using ARProject.Views.RegisterStep;
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
            MeTab.Icon = CreateIcon(FluentUI.person_accounts_24_regular);

            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(WishlistPage), typeof(WishlistPage));
            Routing.RegisterRoute(nameof(ARPage), typeof(ARPage));
            Routing.RegisterRoute(nameof(PaymentPage), typeof(PaymentPage));
            Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(RegisterStep1), typeof(RegisterStep1));
            Routing.RegisterRoute(nameof(RegisterStep2), typeof(RegisterStep2));

            Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));

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

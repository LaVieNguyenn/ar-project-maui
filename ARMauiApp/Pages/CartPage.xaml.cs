using Android.Util;
using ARMauiApp.Unity;
using ARMauiApp.ViewModels;
using Com.Unity3d.Player;
#if ANDROID
using Android.App;
using Android.Content;
using Android.OS;
#endif

namespace ARMauiApp.Pages
{
    public partial class CartPage : ContentPage
    {
        private readonly CartViewModel _viewModel;

        public CartPage(CartViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Log.Debug("CartPage", "OnAppearing called");
            _viewModel.RefreshCartCommand.Execute(null);
        }

    }
}

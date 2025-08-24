using ARMauiApp.Unity;
using ARMauiApp.ViewModels;
#if __ANDROID__
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Com.Unity3d.Player;
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
#if ANDROID
            Log.Debug("CartPage", "OnAppearing called");
#endif            
            _viewModel.RefreshCartCommand.Execute(null);
        }

    }
}

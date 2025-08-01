using ARMauiApp.ViewModels;
using CommunityToolkit.Maui.Alerts;

namespace ARMauiApp.Pages;

public partial class AccountPage : ContentPage
{
    private readonly AccountViewModel _viewModel;

    public AccountPage(AccountViewModel accountViewModel)
    {
        InitializeComponent();
        _viewModel = accountViewModel;
        BindingContext = accountViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.LoadUserCommand.CanExecute(null))
        {
            _viewModel.LoadUserCommand.Execute(null);
        }
    }

    private async void OnEditProfileTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("editprofile");
    }

    private void OnChangePasswordTapped(object sender, EventArgs e)
    {
        if (_viewModel.ChangePasswordCommand.CanExecute(null))
        {
            _viewModel.ChangePasswordCommand.Execute(null);
        }
    }

    private async void OnSettingsTapped(object sender, EventArgs e)
    {
        await DisplayAlert("Cài đặt", "Tính năng này sẽ được triển khai sớm.", "OK");
    }

    private async void OnHelpTapped(object sender, EventArgs e)
    {
        await DisplayAlert("Trợ giúp & Hỗ trợ", "Để được hỗ trợ, vui lòng liên hệ: support@arproject.com", "OK");
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Đăng xuất", "Bạn có chắc chắn muốn đăng xuất không?", "Có", "Không");
        if (result && _viewModel.LogoutCommand.CanExecute(null))
        {
            _viewModel.LogoutCommand.Execute(null);
        }
    }
}

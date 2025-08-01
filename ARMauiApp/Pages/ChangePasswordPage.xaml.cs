using ARMauiApp.Models;
using ARMauiApp.Services;
using CommunityToolkit.Maui.Alerts;

namespace ARMauiApp.Pages;

public partial class ChangePasswordPage : ContentPage
{
    private readonly UserService _userService;
    private readonly TokenService _tokenService;

    public ChangePasswordPage()
    {
        InitializeComponent();

        // Initialize services (in a real app, use dependency injection)
        _tokenService = new TokenService();
        _userService = new UserService(_tokenService);
    }

    private async void OnChangePasswordClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CurrentPasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(NewPasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
        {
            await Toast.Make("Vui lòng điền đầy đủ thông tin").Show();
            return;
        }

        if (NewPasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            await Toast.Make("Mật khẩu mới và xác nhận mật khẩu không khớp").Show();
            return;
        }

        if (NewPasswordEntry.Text.Length < 6)
        {
            await Toast.Make("Mật khẩu mới phải có ít nhất 6 ký tự").Show();
            return;
        }

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        ChangePasswordButton.IsEnabled = false;

        try
        {
            var changePasswordDto = new UserChangePasswordDto
            {
                CurrentPassword = CurrentPasswordEntry.Text,
                NewPassword = NewPasswordEntry.Text,
                ConfirmPassword = ConfirmPasswordEntry.Text
            };

            var success = await _userService.ChangePasswordAsync(changePasswordDto);

            if (success)
            {
                await Toast.Make("Đổi mật khẩu thành công!").Show();
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Toast.Make("Đổi mật khẩu thất bại. Vui lòng kiểm tra mật khẩu hiện tại.").Show();
            }
        }
        catch (Exception ex)
        {
            await Toast.Make($"Lỗi đổi mật khẩu: {ex.Message}").Show();
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            ChangePasswordButton.IsEnabled = true;
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}

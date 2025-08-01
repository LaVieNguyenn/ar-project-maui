using ARMauiApp.Models;
using ARMauiApp.Services;
using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;

namespace ARMauiApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool isLoading = false;

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public LoginViewModel(TokenService tokenService, AuthService authService)
        {
            // Initialize services (in a real app, use dependency injection)
            _tokenService = tokenService;
            _authService = authService;

            // Đăng ký commands trong constructor
            LoginCommand = new AsyncRelayCommand(Login);
            NavigateToRegisterCommand = new AsyncRelayCommand(NavigateToRegister);
        }

        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Toast.Make("Vui lòng nhập email và mật khẩu").Show();
                return;
            }

            IsLoading = true;

            try
            {
                var loginDto = new UserLoginDto
                {
                    Email = Email,
                    Password = Password
                };

                var (success, user) = await _authService.LoginAsync(loginDto);

                if (success)
                {
                    var message = user != null
                        ? $"Chào mừng {user.Username}!"
                        : "Đăng nhập thành công!";

                    await Toast.Make(message).Show();
                    // Navigate to main page
                    await Shell.Current.GoToAsync("//products");
                }
                else
                {
                    await Toast.Make("Email hoặc mật khẩu không hợp lệ").Show();
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Đăng nhập thất bại: {ex.Message}").Show();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task NavigateToRegister()
        {
            await Shell.Current.GoToAsync("//register");
        }
    }
}

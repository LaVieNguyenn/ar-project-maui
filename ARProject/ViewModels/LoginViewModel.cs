using ARProject.Models;
using ARProject.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ARProject.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEmailEntered))]
        string email = string.Empty;

        [ObservableProperty]
        string password = string.Empty;

        public bool IsEmailEntered => !string.IsNullOrWhiteSpace(Email);

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
        }

        [RelayCommand]
        async Task LoginAsync()
        {
            IsLoading = true;

            var response = await _userService.LoginAsync(new LoginRequest { Email = this.Email, Password = this.Password });
            if (response.Success)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Shell.Current.DisplayAlert(
    "Login Failed", "Email or password is incorrect.", "OK");
            }

            IsLoading = false;
        }

        bool CanLogin() => IsNotBusy && IsNotLoading
              && !string.IsNullOrWhiteSpace(Email)
              && !string.IsNullOrWhiteSpace(Password);
    }
}

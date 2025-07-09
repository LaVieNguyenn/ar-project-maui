using ARProject.Models;
using ARProject.Services.UserServices;
using ARProject.Views.RegisterStep;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ARProject.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanLogin))]
        [NotifyPropertyChangedFor(nameof(IsEmailEntered))]
        private string email = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanLogin))]
        private string password = string.Empty;


        public bool IsEmailEntered => !string.IsNullOrWhiteSpace(Email);

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        async Task LoginAsync()
        {
            IsLoading = true;

            var response = await _userService.LoginAsync(new LoginRequest { Email = this.Email, Password = this.Password });
            if (response.Success && !string.IsNullOrEmpty(response.Data))
            {
                await _userService.SaveTokenAsync(response.Data);

               // await _userService.GetProfileAsync();

                await Shell.Current.GoToAsync("//MePage", true);
            }
            else
            {
                await Shell.Current.DisplayAlert("Login Failed", "Email or password is incorrect.", "OK");
            }
            IsLoading = false;
        }

        [RelayCommand]
        async Task NavigateToSignUp()
        {
            await Shell.Current.GoToAsync($"/{nameof(RegisterPage)}");
        }


        [RelayCommand]
        async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        public bool CanLogin => IsNotBusy && IsNotLoading
              && !string.IsNullOrWhiteSpace(Email)
              && !string.IsNullOrWhiteSpace(Password);
    }
}

using ARProject.BLL.Services.UserService;
using ARProject.DAL.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

            User? user = await _userService.AuthenticateAsync(Email, Password);
            if (user != null)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }else
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

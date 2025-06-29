using ARProject.Views;
using ARProject.Views.RegisterStep;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace ARProject.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        [ObservableProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email = string.Empty;

        [ObservableProperty]
        [Required(ErrorMessage = "Password is required")]
        public string password = string.Empty;

        partial void OnEmailChanged(string value)
        {
            ValidateProperty(value, nameof(Email));
        }

        partial void OnPasswordChanged(string value)
        {
            ValidateProperty(value, nameof(Password));
        }


        [RelayCommand]       
        public async Task NavigateToSignIn()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);  
        }

        [RelayCommand]
        public async Task SignUp()
        {
            ValidateAllProperties();
            if (HasErrors) return;

            await Shell.Current.GoToAsync(nameof(RegisterStep1) , true, new Dictionary<string, object>
            {
                {"email", Email },
                {"password", Password}
            });
        }

        [RelayCommand]
        public async Task Back()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}", true);
        }
    }
}
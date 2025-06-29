using ARProject.Models;
using ARProject.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace ARProject.ViewModels
{
    [QueryProperty(nameof(Email), "email")]
    [QueryProperty(nameof(Password), "password")]
    [QueryProperty(nameof(UserName), "userName")]
    [QueryProperty(nameof(Birthday), "birthday")]
    [QueryProperty(nameof(Gender), "gender")]
    public partial class RegisterStep2ViewModel : BaseViewModel
    {
        [ObservableProperty]
        public RegisterRequest userRegister = new();

        [ObservableProperty]
        [Required(ErrorMessage = "Height is required")]
        [Range(1, 300, ErrorMessage = "Height must be between 1 and 300 cm")]
        double height;

        [ObservableProperty]
        [Required(ErrorMessage = "Weight is required")]
        [Range(1, 500, ErrorMessage = "Weight must be between 1 and 500 kg")]
        double weight;

        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string userName;
        [ObservableProperty]
        DateOnly birthday;
        [ObservableProperty]
        int gender;



        partial void OnHeightChanged(double value)
        {
            ValidateProperty(value, nameof(Height));
        }

        partial void OnWeightChanged(double value)
        {
            ValidateProperty(value, nameof(Weight));
        }


        private IUserService _service;

        public RegisterStep2ViewModel(IUserService userService)
        {
            _service = userService;
        }

        [RelayCommand]
        async Task SignUp()
        {
            ValidateAllProperties();
            if (HasErrors) return;

            UserRegister.Email = Email;
            UserRegister.Password = Password;
            UserRegister.UserName = UserName;
            UserRegister.Birthday = Birthday;
            UserRegister.Gender = Gender;
            UserRegister.Height = Height;
            UserRegister.Weight = Weight;

            IsBusy = true;

            try
            {
                var response = await _service.RegisterAsync(userRegister);

                if (response?.Success == true)
                {
                    await Shell.Current.DisplayAlert("Success", "Registration successful! Please login.", "OK");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
                else
                {
                    string error = response?.Message ?? "Registration failed. Please check your information.";
                    await Shell.Current.DisplayAlert("Error", error, "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        [RelayCommand]
        async Task Back()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        async Task ShowWeightInfo()
        {
            await Shell.Current.DisplayAlert("Why do we ask this?", "We use height and weight to recommend accurate clothing sizes using AR.", "OK");
        }
    }
}

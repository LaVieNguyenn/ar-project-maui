using ARProject.Helpers;
using ARProject.Views.RegisterStep;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace ARProject.ViewModels
{
    [QueryProperty(nameof(Email), "email")]
    [QueryProperty(nameof(Password), "password")]
    public partial class RegisterStep1ViewModel : BaseViewModel
    {
        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        [Required(ErrorMessage = "User Name is required")]
        string userName;

        [ObservableProperty]
        [Required(ErrorMessage = "Birthday is required")]
        DateTime birthday = DateTime.Now;

        [ObservableProperty]
        [Required(ErrorMessage = "Gender is required")]
        int gender;

        partial void OnUserNameChanged(string value)
        {
            ValidateProperty(value, nameof(UserName));
        }

        partial void OnBirthdayChanged(DateTime value)
        {
            ValidateProperty(value, nameof(Birthday));
        }

        partial void OnGenderChanged(int value)
        {
            ValidateProperty(value, nameof(Gender));
        }



        public List<string> GenderOptions { get; } = Enum.GetNames(typeof(Enums.Gender)).ToList();

        [RelayCommand]
        async Task Next()
        {
            await Shell.Current.GoToAsync(nameof(RegisterStep2), true, new Dictionary<string, object>
            {
                {"email", Email },
                {"password", Password },
                {"userName", UserName },
                {"birthday", DateOnly.FromDateTime(Birthday)},
                {"gender", Gender }
            });
        }

        [RelayCommand]
        async Task Back()
        {
            await Shell.Current.GoToAsync("..", true);
        }

    }
}

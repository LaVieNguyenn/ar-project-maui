using ARProject.ViewModels;
using ARProject.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace ARProject.ViewModels;

public partial class MeViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    [ObservableProperty] string userName;
    [ObservableProperty] string gender;
    [ObservableProperty] double height;
    [ObservableProperty] double weight;
    [ObservableProperty] bool isLoggedIn;

    public MeViewModel(IUserService userService)
    {
        _userService = userService;
        LoadUserInfo();
    }


    public string LoginLogoutButtonText => IsLoggedIn ? "Log out" : "Log in";

    public async void LoadUserInfo()
    {
        IsLoggedIn = _userService.IsLoggedIn();

        if (IsLoggedIn)
        {
            var user = await _userService.LoadLocalProfileAsync() ?? await _userService.GetProfileAsync();
            if (user != null)
            {
                UserName = user.UserName ?? "User";
                Gender = user.Gender switch
                {
                    1 => "Male",
                    2 => "Female",
                    _ => "-"
                };
                Height = user.Height;
                Weight = user.Weight;
            }
            else
            {
                UserName = "Guest";
                Gender = "-";
                Height = 0;
                Weight = 0;
            }
        }
        else
        {
            UserName = "Guest";
            Gender = "-";
            Height = 0;
            Weight = 0;
        }
    }

    [RelayCommand]
    async Task EditProfile()
    {
        if (!IsLoggedIn)
        {
            await Shell.Current.GoToAsync("/LoginPage");
        }
        else
        {
            await Shell.Current.GoToAsync("EditProfilePage");
        }
    }

    [RelayCommand]
    async Task LoginLogout()
    {
        if (IsLoggedIn)
        {
            await _userService.LogoutAsync();
            IsLoggedIn = false;
            LoadUserInfo(); // Gọi lại để reset
            await Shell.Current.DisplayAlert("Announcement", "Logout successfully!", "OK");
        }
        else
        {
            await Shell.Current.GoToAsync("/LoginPage");
        }
    }
}

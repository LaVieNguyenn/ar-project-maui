using ARProject.Services.UserServices;
using ARProject.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ARProject.ViewModels;

public partial class MeViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    [ObservableProperty] string displayName;
    [ObservableProperty] string email;
    [ObservableProperty] string avatarUrl;
    [ObservableProperty] bool isLoggedIn;

    public string LoginLogoutButtonText => IsLoggedIn ? "Log out" : "Log in";

    public MeViewModel(IUserService userService)
    {
        _userService = userService;
        LoadUserInfo();
    }

    private async void LoadUserInfo()
    {
        IsLoggedIn = _userService.IsLoggedIn();
        if (IsLoggedIn)
        {
            //var user = await _userService.LoadLocalProfileAsync() ?? await _userService.GetProfileAsync();
            //if (user != null)
            //{
            //    DisplayName = user.DisplayName ?? "Người dùng";
            //    Email = user.Email;
            //    AvatarUrl = string.IsNullOrWhiteSpace(user.AvatarUrl) ? "user_placeholder.png" : user.AvatarUrl;
            //}
            DisplayName = "User_demo";
            Email = "demo gmail";
            AvatarUrl = "user_placeholder.png";
        }
        else
        {
            DisplayName = "Guest";
            Email = "";
            AvatarUrl = "user_placeholder.png";
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
            DisplayName = "Guest";
            Email = "";
            AvatarUrl = "user_placeholder.png";
            await Shell.Current.DisplayAlert("Anncouncement", "Logout successfully!", "OK");
        }
        else
        {
            await Shell.Current.GoToAsync("/LoginPage");
        }
    }
}

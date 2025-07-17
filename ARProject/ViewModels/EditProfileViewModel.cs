using ARProject.Models;
using ARProject.Services.UserServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ARProject.ViewModels;

public partial class EditProfileViewModel : ObservableObject
{
    private readonly IUserService _userService;

    [ObservableProperty] string userName;
    [ObservableProperty] int gender;
    [ObservableProperty] double height;
    [ObservableProperty] double weight;

    public ICommand SaveProfileCommand { get; }

    public EditProfileViewModel(IUserService userService)
    {
        _userService = userService;

        // Tạo command thủ công thay vì [RelayCommand]
        SaveProfileCommand = new AsyncRelayCommand(SaveProfileAsync);

        LoadUser();
    }

    private async void LoadUser()
    {
        var user = await _userService.LoadLocalProfileAsync();
        if (user != null)
        {
            UserName = user.UserName;
            Gender = user.Gender;
            Height = user.Height;
            Weight = user.Weight;
        }
    }

    private async Task SaveProfileAsync()
    {
        var user = await _userService.LoadLocalProfileAsync();
        if (user == null) return;

        user.UserName = UserName;
        user.Gender = Gender;
        user.Height = Height;
        user.Weight = Weight;
        user.AvatarUrl = "";

        var success = await _userService.UpdateProfileAsync(user);

        if (success)
        {
            await Shell.Current.DisplayAlert("Success", "Profile updated!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Update failed!", "OK");
        }
    }
}

using ARMauiApp.Models;
using ARMauiApp.Pages;
using ARMauiApp.Services;
using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;

namespace ARMauiApp.ViewModels
{
    public partial class AccountViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly AuthService _authService;

        [ObservableProperty]
        private UserDetailDto? currentUser;

        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string avatarUrl = string.Empty;

        [ObservableProperty]
        private int gender = 0;

        [ObservableProperty]
        private double height = 0;

        [ObservableProperty]
        private double weight = 0;

        [ObservableProperty]
        private bool isLoading = false;

        [ObservableProperty]
        private bool isEditMode = false;

        public List<string> GenderOptions { get; } = new List<string> { "Khác", "Nam", "Nữ" };

        public ICommand LoadUserCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ChangePasswordCommand { get; }

        public AccountViewModel(UserService userService, TokenService tokenService, AuthService authService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _authService = authService;

            LoadUserCommand = new AsyncRelayCommand(LoadUser);
            EditCommand = new RelayCommand(EnableEdit);
            SaveCommand = new AsyncRelayCommand(SaveUser);
            CancelCommand = new RelayCommand(CancelEdit);
            LogoutCommand = new AsyncRelayCommand(Logout);
            ChangePasswordCommand = new AsyncRelayCommand(ChangePassword);
        }

        private async Task LoadUser()
        {
            IsLoading = true;
            try
            {
                CurrentUser = await _userService.GetCurrentUserAsync();
                if (CurrentUser != null)
                {
                    Username = CurrentUser.Username ?? string.Empty;
                    Email = CurrentUser.Email ?? string.Empty;
                    AvatarUrl = CurrentUser.AvatarUrl ?? string.Empty;
                    Gender = CurrentUser.Gender;
                    Height = CurrentUser.Height;
                    Weight = CurrentUser.Weight;
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Lỗi tải thông tin: {ex.Message}").Show();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void EnableEdit()
        {
            IsEditMode = true;
        }

        private async Task SaveUser()
        {
            if (CurrentUser == null) return;

            IsLoading = true;
            try
            {
                var updateDto = new UserUpdateDto
                {
                    Username = Username,
                    Email = Email,
                    AvatarUrl = AvatarUrl,
                    Gender = Gender,
                    Height = Height,
                    Weight = Weight
                };

                var updatedUser = await _userService.UpdateUserAsync(CurrentUser.Id!, updateDto);
                if (updatedUser != null)
                {
                    CurrentUser = updatedUser;
                    IsEditMode = false;
                    await Toast.Make("Cập nhật thông tin thành công!").Show();
                }
                else
                {
                    await Toast.Make("Cập nhật thông tin thất bại!").Show();
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Lỗi cập nhật: {ex.Message}").Show();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void CancelEdit()
        {
            if (CurrentUser != null)
            {
                Username = CurrentUser.Username ?? string.Empty;
                Email = CurrentUser.Email ?? string.Empty;
                AvatarUrl = CurrentUser.AvatarUrl ?? string.Empty;
                Gender = CurrentUser.Gender;
                Height = CurrentUser.Height;
                Weight = CurrentUser.Weight;
            }
            IsEditMode = false;
        }

        private async Task Logout()
        {
            try
            {
                await _authService.LogoutAsync();
                await Toast.Make("Đăng xuất thành công!").Show();
                await Shell.Current.GoToAsync("//login");
            }
            catch (Exception ex)
            {
                await Toast.Make($"Lỗi đăng xuất: {ex.Message}").Show();
            }
        }

        private async Task ChangePassword()
        {
            // Navigate to change password page
            await Shell.Current.GoToAsync(nameof(ChangePasswordPage));
        }

        public string GenderText => Gender switch
        {
            1 => "Nam",
            2 => "Nữ",
            _ => "Khác"
        };
    }
}

using ARMauiApp.Models;
using ARMauiApp.Services;
using CommunityToolkit.Maui.Alerts;

namespace ARMauiApp.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private DateTime birthday = DateTime.Today.AddYears(-20);

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private int selectedGender = 0;

        [ObservableProperty]
        private double height = 170;

        [ObservableProperty]
        private double weight = 70;

        [ObservableProperty]
        private bool isLoading = false;

        public List<string> GenderOptions { get; } = new() { "Nam", "Nữ", "Khác" };

        public DateTime MaxBirthday { get; } = DateTime.Today.AddYears(-13); // Minimum age 13

        public RegisterViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        async Task Register()
        {
            if (!ValidateInput())
                return;

            IsLoading = true;

            try
            {
                var registerDto = new UserRegisterDto
                {
                    Username = Username,
                    Email = Email,
                    Birthday = DateOnly.FromDateTime(Birthday),
                    Password = Password,
                    Gender = SelectedGender,
                    Height = Height,
                    Weight = Weight,
                    RoleId = "661fcf75e40000551e02a002"  // Default user role
                };

                var success = await _authService.RegisterAsync(registerDto);

                if (success)
                {
                    await Toast.Make("Đăng ký thành công! Vui lòng đăng nhập.").Show();
                    await Shell.Current.GoToAsync("//login");
                }
                else
                {
                    await Toast.Make("Đăng ký thất bại. Email có thể đã được sử dụng.").Show();
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Lỗi kết nối: {ex.Message}").Show();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                Toast.Make("Vui lòng nhập tên người dùng").Show();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                Toast.Make("Vui lòng nhập email").Show();
                return false;
            }

            if (!IsValidEmail(Email))
            {
                Toast.Make("Email không hợp lệ").Show();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                Toast.Make("Vui lòng nhập mật khẩu").Show();
                return false;
            }

            if (Password.Length < 6)
            {
                Toast.Make("Mật khẩu phải có ít nhất 6 ký tự").Show();
                return false;
            }

            var age = DateTime.Today.Year - Birthday.Year;
            if (Birthday.Date > DateTime.Today.AddYears(-age)) age--;

            if (age < 13)
            {
                Toast.Make("Bạn phải từ 13 tuổi trở lên để đăng ký").Show();
                return false;
            }

            if (Height < 50 || Height > 300)
            {
                Toast.Make("Chiều cao phải từ 50-300cm").Show();
                return false;
            }

            if (Weight < 20 || Weight > 500)
            {
                Toast.Make("Cân nặng phải từ 20-500kg").Show();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [RelayCommand]
        async Task NavigateToLogin()
        {
            await Shell.Current.GoToAsync("//login");
        }
    }
}

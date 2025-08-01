using ARMauiApp.Configuration;
using ARMauiApp.Models;

namespace ARMauiApp.Services
{
    public class AuthService : BaseApiService
    {
        private readonly UserService _userService;

        public AuthService(TokenService tokenService, UserService userService) : base(tokenService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<(bool Success, UserDetailDto? User)> LoginAsync(UserLoginDto loginDto)
        {
            try
            {
                var token = await PostAsync<string>(ApiConfig.Endpoints.Login, loginDto);

                if (!string.IsNullOrEmpty(token))
                {
                    await _tokenService.SaveTokenAsync(token);
                    var user = await _userService.GetCurrentUserAsync();
                    return (true, user);
                }
                else
                {
                    await _tokenService.ClearTokenAsync();
                }

                return (false, null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
                return (false, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        public async Task<bool> RegisterAsync(UserRegisterDto registerDto)
        {
            try
            {
                return await PostAsync(ApiConfig.Endpoints.Register, registerDto);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Registration error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra authentication hiện tại
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsAuthenticated, UserDetailDto? User)> CheckAuthenticationAsync()
        {
            try
            {
                if (!await _tokenService.IsTokenValidAsync())
                {
                    return (false, null);
                }

                var user = await _userService.GetCurrentUserAsync();
                return (user != null, user);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Authentication check error: {ex.Message}");
                await _tokenService.ClearTokenAsync();
                return (false, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            await _tokenService.ClearTokenAsync();
        }
    }
}

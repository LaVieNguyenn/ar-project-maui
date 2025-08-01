using ARMauiApp.Configuration;
using ARMauiApp.Models;

namespace ARMauiApp.Services
{
    public class UserService : BaseApiService
    {
        public UserService(TokenService tokenService) : base(tokenService)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserDetailDto?> GetUserByIdAsync(string userId)
        {
            try
            {
                return await GetAsync<UserDetailDto>($"{ApiConfig.Endpoints.Users}/{userId}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Get user error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        public async Task<UserDetailDto?> UpdateUserAsync(string userId, UserUpdateDto updateDto)
        {
            try
            {
                var response = await PutAsync<UserDetailDto>($"{ApiConfig.Endpoints.Users}/{userId}", updateDto);
                return response;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Update user error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <returns></returns>
        public async Task<bool> ChangePasswordAsync(UserChangePasswordDto changePasswordDto)
        {
            try
            {
                return await PutAsync($"{ApiConfig.Endpoints.Users}/change-password", changePasswordDto);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Change password error: {ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// Lấy ra thông tin người dùng hiện tại
        /// </summary>
        /// <returns></returns>
        public async Task<UserDetailDto?> GetCurrentUserAsync()
        {
            try
            {
                return await GetAsync<UserDetailDto>($"{ApiConfig.Endpoints.Users}/me");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Get user error: {ex.Message}");
                return null;
            }
        }
    }
}

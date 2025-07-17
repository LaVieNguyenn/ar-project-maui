using ARProject.Helpers;
using ARProject.Models;
using System.Text.Json;

namespace ARProject.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;
        private const string TokenKey = "USER_TOKEN";
        private const string ProfileKey = "USER_PROFILE";

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<ApiResponse<string>> LoginAsync(LoginRequest rq) => _apiService.PostAsync<LoginRequest, ApiResponse<string>>(ConstData.Api.LOGIN, rq);
        public Task<ApiResponse<string>> RegisterAsync(RegisterRequest rq) => _apiService.PostAsync<RegisterRequest, ApiResponse<string>>(ConstData.Api.REGISTER, rq);
        public string GetToken() => SecureStorage.GetAsync(TokenKey).GetAwaiter().GetResult();
        public async Task SaveTokenAsync(string token) => await SecureStorage.SetAsync(TokenKey, token);
        public async Task RemoveTokenAsync()
        {
            SecureStorage.Remove(TokenKey);
            SecureStorage.Remove(ProfileKey);
        }
        public bool IsLoggedIn()
        {
            var token = SecureStorage.GetAsync(TokenKey).GetAwaiter().GetResult();
            return !string.IsNullOrEmpty(token);
        }

        public async Task<UserProfile> GetProfileAsync()
        {
            var token = await SecureStorage.GetAsync(TokenKey);
            if (string.IsNullOrEmpty(token))
                return null;

            _apiService.SetBearerToken(token);

            var userId = JwtHelper.ExtractUserId(token); 
            if (string.IsNullOrEmpty(userId))
                return null;

            var response = await _apiService.GetAsync<ApiResponse<UserProfile>>(
                ConstData.Api.GET_USER_BY_ID + userId
            );

            if (response?.Success == true && response.Data != null)
            {
                await SaveProfileAsync(response.Data);
                return response.Data;
            }

            return null;
        }

        public async Task<bool> UpdateProfileAsync(UserProfile updatedProfile)
        {
            var token = await SecureStorage.GetAsync(TokenKey);
            if (string.IsNullOrEmpty(token)) return false;

            _apiService.SetBearerToken(token);

            var success = await _apiService.PutAsync(
                ConstData.Api.UPDATE_USER_BY_ID + updatedProfile.Id,
                updatedProfile
            );

            // Nếu PUT thành công → gọi lại GetProfile để lấy bản mới nhất
            if (success)
            {
                var refreshed = await GetProfileAsync();
                return refreshed != null;
            }

            return false;
        }


        public async Task SaveProfileAsync(UserProfile user)
        {
            var json = JsonSerializer.Serialize(user);
            await SecureStorage.SetAsync(ProfileKey, json);
        }

        public async Task<UserProfile> LoadLocalProfileAsync()
        {
            var json = await SecureStorage.GetAsync(ProfileKey);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonSerializer.Deserialize<UserProfile>(json);
        }

        public async Task LogoutAsync()
        {
            await RemoveTokenAsync();
        }

    }
}

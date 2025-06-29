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
        //public async Task<UserInfo> GetProfileAsync()
        //{
        //    var token = await SecureStorage.GetAsync(TokenKey);
        //    if (string.IsNullOrEmpty(token)) return null;

        //    // Đặt token vào header gọi API
        //    _apiService.SetBearerToken(token);

        //    var user = await _apiService.GetAsync<UserInfo>("/api/v1/users/me");
        //    if (user != null)
        //        await SaveProfileAsync(user);

        //    return user;
        //}
        //public async Task SaveProfileAsync(UserInfo user)
        //{
        //    var json = JsonSerializer.Serialize(user);
        //    await SecureStorage.SetAsync(ProfileKey, json);
        //}

        //public async Task<UserInfo> LoadLocalProfileAsync()
        //{
        //    var json = await SecureStorage.GetAsync(ProfileKey);
        //    if (string.IsNullOrEmpty(json)) return null;
        //    return JsonSerializer.Deserialize<UserInfo>(json);
        //}

        public async Task LogoutAsync()
        {
            await RemoveTokenAsync();
        }

    }
}

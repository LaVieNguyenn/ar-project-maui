using ARProject.Helpers;
using ARProject.Models;

namespace ARProject.Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResponse<string>> LoginAsync(LoginRequest rq);
        Task<ApiResponse<string>> RegisterAsync(RegisterRequest rq);
        string GetToken();
        Task SaveTokenAsync(string token);
        Task RemoveTokenAsync();

        Task<UserProfile> GetProfileAsync();

        Task<bool> UpdateProfileAsync(UserProfile updatedProfile);

        Task SaveProfileAsync(UserProfile user); 
        Task<UserProfile> LoadLocalProfileAsync();

        bool IsLoggedIn();
        Task LogoutAsync();
    }
}

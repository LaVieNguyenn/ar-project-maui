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

        //Task<UserInfo> GetProfileAsync();
        //Task SaveProfileAsync(UserInfo user); 
        //Task<UserInfo> LoadLocalProfileAsync();

        bool IsLoggedIn();
        Task LogoutAsync();
    }
}

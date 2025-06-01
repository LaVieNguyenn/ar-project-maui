using ARProject.DAL.Models;

namespace ARProject.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string email, string password);
    }
}

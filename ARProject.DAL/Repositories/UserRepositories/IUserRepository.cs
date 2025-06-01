using ARProject.DAL.Models;

namespace ARProject.DAL.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAndPasswordAsync(string email, string password);
    }
}

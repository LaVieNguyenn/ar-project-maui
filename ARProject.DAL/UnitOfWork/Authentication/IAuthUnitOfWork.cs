using ARProject.DAL.Repositories.UserRepositories;

namespace ARProject.DAL.UnitOfWork.Authentication
{
    public interface IAuthUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
    }
}

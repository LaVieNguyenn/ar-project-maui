using ARProject.DAL.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.DAL.UnitOfWork.Authentication
{
    public interface IAuthUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
    }
}

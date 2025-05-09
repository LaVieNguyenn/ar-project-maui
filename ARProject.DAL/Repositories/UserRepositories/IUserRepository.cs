using ARProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.DAL.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAndPasswordAsync(string email, string password);
    }
}

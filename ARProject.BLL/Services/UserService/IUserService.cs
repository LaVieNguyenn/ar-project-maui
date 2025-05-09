using ARProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string email, string password);
    }
}

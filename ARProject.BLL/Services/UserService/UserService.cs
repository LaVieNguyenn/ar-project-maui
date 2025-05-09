using ARProject.DAL.Models;
using ARProject.DAL.UnitOfWork.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.BLL.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IAuthUnitOfWork _uow;
        public UserService(IAuthUnitOfWork uow) => _uow = uow;
        public Task<User?> AuthenticateAsync(string email, string password)
        {
            return _uow.Users.GetByEmailAndPasswordAsync(email, password);
        }
    }
}

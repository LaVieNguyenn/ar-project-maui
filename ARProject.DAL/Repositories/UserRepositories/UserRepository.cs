using ARProject.DAL.Helper;
using ARProject.DAL.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.DAL.Repositories.UserRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IMongoDatabase db) : base(db, DbNameHelper.DbCollectionName.USER)
        { }

        public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email) &
                         Builders<User>.Filter.Eq(u => u.Password, password);
            return await GetByFilterAsync(filter);
        }
    }
}

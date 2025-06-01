using ARProject.DAL.Helper;
using ARProject.DAL.Repositories.UserRepositories;
using MongoDB.Driver;

namespace ARProject.DAL.UnitOfWork.Authentication
{
    public class AuthUnitOfWork : IAuthUnitOfWork
    {
        private readonly IMongoDatabase _db;
        private IUserRepository? _userRepo;

        public AuthUnitOfWork(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(DbNameHelper.DbName.AUTH);
        }

        public IUserRepository Users => _userRepo ??= new UserRepository(_db);


        public void Dispose() { }
    }
}

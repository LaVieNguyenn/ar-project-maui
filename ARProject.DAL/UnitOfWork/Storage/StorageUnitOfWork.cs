using ARProject.DAL.Helper;
using MongoDB.Driver;

namespace ARProject.DAL.UnitOfWork.Storage
{
    public class StorageUnitOfWork : IStorageUnitOfWork
    {
        private readonly IMongoDatabase _db;
        public StorageUnitOfWork(string conn) =>
            _db = new MongoClient(conn).GetDatabase(DbNameHelper.DbName.STORAGE);

        public void Dispose() { }
    }
}

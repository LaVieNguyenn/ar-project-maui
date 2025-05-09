using ARProject.DAL.Helper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

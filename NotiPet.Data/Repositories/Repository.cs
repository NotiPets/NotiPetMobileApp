using System;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using Realms.Schema;
using Realms.Sync;

namespace NotiPet.Data.Repositories
{
    public interface IDataSource<T> where T : RealmObject
    {
        public void AddOrUpdate(T dto);
        public void Delete(T dto);
        public void DeleteAll();
        public IQueryable<T> GetAll();
        
        
    }

    public interface IDataBaseProvider<T>
    {
        public T Database { get; }
        void CleanDatabase();
        void CloseDatabase();
        Task ConfigurationProfile(string username,string token);

    }

    public class RealmDatabaseProvider : IDataBaseProvider<Realm>
    {
        public Realm Database { get; private set; }

        public RealmDatabaseProvider()
        {

        }
        public void CleanDatabase()
        {
            Realm.DeleteRealm(Database.Config);
        }

        public void CloseDatabase()
            => Database.Dispose();
        

        public  async Task ConfigurationProfile(string username,string token)
        {
            var config = new RealmConfiguration($"{username}.realm")
            {
                SchemaVersion = 1,
            };
            Database = await Realm.GetInstanceAsync(config);
        }
    }
    public class Repository<TDto>:IDataSource<TDto>
         where TDto:RealmObject
    {
        private readonly Realm _realm;

        public Repository(IDataBaseProvider<Realm> realm)
        {
            _realm = realm.Database;
        }

        public void AddOrUpdate(TDto dto)
        {
            _realm.Write(() =>
            {
                _realm.Add(dto,true);
            });
        }

        public void Delete(TDto dto)
        {
            _realm.Write(() =>
            {
                _realm.Remove(dto);
            });
        }

        public void DeleteAll()
        {
            _realm.Write(() =>
            {
                _realm.RemoveAll<TDto>();
            });
        }

        public IQueryable<TDto> GetAll()
            => _realm.All<TDto>();
        
 
    }
}
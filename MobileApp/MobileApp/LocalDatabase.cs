using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace MobileApp
{
    public class LocalDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public LocalDatabase(string dbpath) 
        {
            _database = new SQLiteAsyncConnection(dbpath);
            _database.CreateTableAsync<LocalData>();

                _database.InsertAsync(new LocalData
                {
                    Login = null,
                    FingerPrint = false,
                    IsRemembered = false
                });
        }

        public Task<LocalData> GetLocalData() {
            return _database.Table<LocalData>().FirstOrDefaultAsync();
        }

        public Task<int> SaveLocalData(LocalData local) {
            return _database.InsertAsync(local);
        }


        public Task<int> saveLogin(string login) {
            return _database.ExecuteAsync("UPDATE LocalData SET Login = ?", login);
        }
        public Task<int> saveFinger(bool finger)
        {
            return _database.ExecuteAsync("UPDATE LocalData SET FingerPrint = ?", finger);
        }

        public Task<int> saveRemember(bool rem)
        {
            return _database.ExecuteAsync("UPDATE LocalData SET IsRemembered = ?", rem);
        }
        public async Task logOut()
        {
            await saveLogin("");
            await saveFinger(false);
            await saveRemember(false);
        }

    }
}

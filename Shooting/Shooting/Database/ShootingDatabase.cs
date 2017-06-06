using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooting.Database
{
    public class ShootingDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ShootingDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Result>().Wait();
        }

        //GET ALL FIGURJAKT RESULTS
        public Task<List<Result>> GetFigurjaktResults()
        {         
            return database.QueryAsync<Result>("SELECT * FROM [Result] WHERE [Type] = Figurjakt");
        }

        public Task<List<Result>> GetResultByStevneId(string stevneID)
        {
            return database.QueryAsync<Result>("SELECT * FROM [Result] WHERE [StevneID] = " + stevneID);
        }

        public Task<int> SaveResultAsync(Result res)
        {
            if (res.ID != 0)
            {
                return database.UpdateAsync(res);
            }
            else
            {
                return database.InsertAsync(res);
            }
        }

        public Task<int> DeleteResultAsync(Result res)
        {
            return database.DeleteAsync(res);
        }
    }
}

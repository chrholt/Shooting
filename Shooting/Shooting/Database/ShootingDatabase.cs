using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;

namespace Shooting.Database
{
    public class ShootingDatabase
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Result> Results { get; set; }

        public ShootingDatabase()
        {
            database = DependencyService.Get<IFileHelper>().DbConnection();
            database.CreateTable<Result>();
            this.Results = new ObservableCollection<Result>(database.Table<Result>());

            //IF THE TABLE IS EMPTY INITIALIZE THE COLLECTION
            if(!database.Table<Result>().Any())
            {
                AddNewResult();
            }
        }

        private void AddNewResult()
        {
            this.Results.Add(new Result
            {
                Name = "Test result",
               Results = "",
               Date = DateTime.Now,
               StevneID ="TestFJ",
               Type = "Figurjakt" 
            });
        }
        internal Result GetResult(int id)
        {
            lock(collisionLock)
            {
                var query = from res in database.Table<Result>()
                            where res.ID == id
                            select res;
                return query.FirstOrDefault();
            }
        }
        //GET ALL Jaktfelt Results From Database
        internal ObservableCollection<Result> GetJaktfeltResults()
        {
            ObservableCollection<Result> oc = new ObservableCollection<Result>();
            lock (collisionLock)
            {
                var query = from res in database.Table<Result>()
                            where res.Type == "Jaktfelt"
                            select res;
                foreach(var r in query)
                {
                    oc.Add(r);
                }
                
                return oc;
            }
        }
        //GET All Figurjakt Results From Database
        public ObservableCollection<Result> GetFigurjaktResults()
        {
            ObservableCollection<Result> oc = new ObservableCollection<Result>();
            lock (collisionLock)
            {
                var query = from res in database.Table<Result>()
                            where res.Type == "Figurjakt"
                            select res;
                foreach(var r in query)
                {
                    oc.Add(r);
                }
                return oc;
            }
        }
        //GET All Jegertrap Results From Database
        public ObservableCollection<Result> GetJegertrapResults()
        {
            ObservableCollection<Result> oc = new ObservableCollection<Result>();
            lock (collisionLock)
            {
                var query = from res in database.Table<Result>()
                            where res.Type == "Jegertrap"
                            select res;
                foreach (var r in query)
                {
                    oc.Add(r);
                }
                return oc;
            }
        }
        //Common method for saving a result
        public int SaveResult(Result resultInstance)
        {
            lock (collisionLock)
            {
                if(resultInstance.ID != 0)
                {
                    database.Update(resultInstance);
                    return resultInstance.ID;
                }
                else
                {
                    database.Insert(resultInstance);
                    return resultInstance.ID;
                }

            }
        }

        public void SaveAllCustomers()
        {
            lock (collisionLock)
            {
                foreach(var resultInstance in this.Results)
                {
                    if(resultInstance.ID != 0)
                    {
                        database.Update(resultInstance);
                    }
                    else
                    {
                        database.Insert(resultInstance);
                    }
                }
            }
        }
        //Common method for deleting a result
        public int DeleteResult(Result resultInstance)
        {
            var id = resultInstance.ID;
            if(id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Result>(id);
                }
            }
            this.Results.Remove(resultInstance);
            return id;
        }

        public int UpdateResult(Result resultInstance)
        {
            var id = resultInstance.ID;
            if(id != 0)
            {
                lock (collisionLock)
                {
                    database.Update(resultInstance);
                }
            }
            return id;
        }
    }
}

using System;
using System.IO;
using Shooting.Droid;
using Xamarin.Forms;
using SQLite;

[assembly: Dependency(typeof(FileHelper))]
namespace Shooting.Droid
{
    public class FileHelper : IFileHelper
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "Shooting.db3";
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}
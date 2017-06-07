using System;
using System.IO;
using Xamarin.Forms;
using Shooting.iOS;
using SQLite;

[assembly: Dependency(typeof(FileHelper))]
namespace Shooting.iOS
{
    public class FileHelper : IFileHelper
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "Shooting.db3";
            string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}
using System;
using Xamarin.Forms;
using Shooting.UWP;
using Windows.Storage;
using System.IO;
using SQLite;

[assembly: Dependency(typeof(FileHelper))]
namespace Shooting.UWP
{
    public class FileHelper : IFileHelper
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "Shooting.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }
    }
}

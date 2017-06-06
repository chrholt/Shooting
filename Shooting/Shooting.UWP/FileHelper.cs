using System;
using Xamarin.Forms;
using Shooting.UWP;
using Windows.Storage;
using System.IO;

[assembly: Dependency(typeof(FileHelper))]
namespace Shooting.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}

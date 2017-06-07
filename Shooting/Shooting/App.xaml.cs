using Shooting.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Shooting
{
    public partial class App : Application
    {
        static ShootingDatabase database;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            
        }

        public static ShootingDatabase Database
        {
            get
            {
                if(database == null)
                {
                    database = new ShootingDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Shooting.db3"));
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

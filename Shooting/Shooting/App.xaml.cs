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
        //static ShootingDatabase database;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            
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

        public bool DoBack
        {
            get
            {
                MasterDetailPage mainPage = MainPage as MasterDetailPage;
                if(mainPage != null)
                {
                    bool canDoBack = mainPage.Detail.Navigation.NavigationStack.Count > 1 || mainPage.IsPresented;
                    if (!canDoBack)
                    {
                        mainPage.IsPresented = true;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
        }
    }
}

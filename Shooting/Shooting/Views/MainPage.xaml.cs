using Shooting.MenuItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Shooting.Views;

namespace Shooting
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            //FILL MENU WITH LINKS TO PAGES
            var menuList = new List<MasterPageItem>();
            menuList.Add(new MasterPageItem
            {
                Title = "Figurjakt",
                Icon = "icon.png",
                TargetType = typeof(Figurjakt)
            });
            //REMOVE THIS
            menuList.Add(new MasterPageItem
            {
                Title = "Test link",
                Icon = "icon.png",
                TargetType = typeof(Figurjakt)
            });

            menuListView.ItemsSource = menuList;

            //FILL SETTINGS SECTION WITH LINKS TO PAGES
            var settingsSectionList = new List<MasterPageItem>();
            settingsSectionList.Add(new MasterPageItem
            {
                Title = "Innstillinger",
                Icon = "icon.png",
                TargetType = typeof(Settings)
            });


            settingsSectionListView.ItemsSource = settingsSectionList;

            //SETS DETAIL PAGE
            var detailPage = new NavigationPage((Page)Activator.CreateInstance(typeof(FigurjaktCreate)));
            detailPage.BarBackgroundColor = Color.FromHex("#666666");
            detailPage.BarTextColor = Color.FromHex("#FFFFFF");
            Detail = detailPage;


        }

        private void menuListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            var detailPage = new NavigationPage((Page)Activator.CreateInstance(page));
            detailPage.BarBackgroundColor = Color.FromHex("#666666");
            detailPage.BarTextColor = Color.FromHex("#FFFFFF");
            Detail = detailPage;
            IsPresented = false;
        }

        private void settingsSectionListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            var detailPage = new NavigationPage((Page)Activator.CreateInstance(page));
            detailPage.BarBackgroundColor = Color.FromHex("#666666");
            detailPage.BarTextColor = Color.FromHex("#FFFFFF");
            Detail = detailPage;
            IsPresented = false;
        }
    }
}

using Shooting.Database;
using Shooting.ViewsCreate;
using Shooting.ViewsDetails;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Jaktfelt : ContentPage
    {
        private ShootingDatabase database;
        private ObservableCollection<Result> jaktfeltResults;

        public Jaktfelt()
        {
            InitializeComponent();
            this.database = new ShootingDatabase();
            //Adding button to top toolbar
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "plus_round_128x128_white_hollow.png",
                Text = "New Result",
                Command = new Command(this.GoToRegisterJaktfeltResult)
            });
            
        }

        //Go to details page for selected result in listview
        private void jaktfeltResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (Result)e.SelectedItem;
            var newPage = new JaktfeltDetails(selectedItem, jaktfeltResults);
            newPage.BindingContext = selectedItem;
            Navigation.PushAsync(newPage);
        }
        //Go to add new result 
        private void GoToRegisterJaktfeltResult()
        {
            var newPage = new JaktfeltCreate(jaktfeltResults);
            Navigation.PushAsync(newPage);
        }
        //Searchbar text changed method
        private void jaktfeltResultsListViewSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = jaktfeltResultsListViewSearchBar.Text;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                jaktfeltResultsListView.ItemsSource = from f in jaktfeltResults
                                                      where f.Name.Contains(keyword) || f.Date.ToString("dd/MM/yyyy").Contains(keyword)
                                                      select f;
            }
            //figurjaktResults.Where(name => name.Name.Contains(keyword));
            //figurjaktResults.Where(date => date.Date.ToString().Contains(keyword)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Getting results from database
            jaktfeltResults = database.GetJaktfeltResults();
            //Setting the source for the listview items
            jaktfeltResultsListView.ItemsSource = jaktfeltResults;
        }
    }
}

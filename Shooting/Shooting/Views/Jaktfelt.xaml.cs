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

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "plus_round_128x128_white_hollow.png",
                Text = "New Result",
                Command = new Command(this.GoToRegisterJaktfeltResult)
            });

            jaktfeltResults = database.GetJaktfeltResults();
            jaktfeltResultsListView.ItemsSource = jaktfeltResults;
        }


        private void jaktfeltResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (Result)e.SelectedItem;
            var newPage = new JaktfeltDetails(selectedItem, jaktfeltResults);
            newPage.BindingContext = selectedItem;
            Navigation.PushAsync(newPage);
        }

        private void GoToRegisterJaktfeltResult()
        {
            var newPage = new JaktfeltCreate(jaktfeltResults);
            Navigation.PushAsync(newPage);
        }
    }
}

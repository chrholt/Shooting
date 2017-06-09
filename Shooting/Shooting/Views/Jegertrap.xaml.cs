using Shooting.Database;
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
    public partial class Jegertrap : ContentPage
    {
        private ShootingDatabase database;
        private ObservableCollection<Result> jegertrapResults;

        public Jegertrap()
        {
            InitializeComponent();
            this.database = new ShootingDatabase();

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "plus_round_128x128_white_hollow.png",
                Text = "New Result",
                Command = new Command(this.GoToRegisterJegertrapResult)

            });
            jegertrapResults = database.GetFigurjaktResults();
            jegertrapResultsListView.ItemsSource = jegertrapResults;
        }

        private void jegertrapResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (Result)e.SelectedItem;
            //var newPage = new JegertrapDetails(selectedItem, jegertrapResults);
            //newPage.BindingContext = selectedItem;
            //Navigation.PushAsync(newPage);
        }

        private void GoToRegisterJegertrapResult()
        {
            //var newPage = new JegertrapCreate(jegertrapResults);
            //Navigation.PushAsync(newPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}

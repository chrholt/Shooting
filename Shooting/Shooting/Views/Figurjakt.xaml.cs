using Shooting.Database;
using Shooting.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Figurjakt : ContentPage
    {
        private ShootingDatabase database;
        public Figurjakt()
        {
            InitializeComponent();
            this.database = new ShootingDatabase();
            //var figurjaktResults = database.GetFigurjaktResults();

            //    figurjaktResultsListView.ItemsSource = figurjaktResults;
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "icon.png",
                Text = "New Result",
                Command = new Command(this.GoToRegisterFigurjaktResult)
                
            });
        }

        private void figurjaktResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = this.database;
        }

        private void GoToRegisterFigurjaktResult()
        {
            var newPage = new NavigationPage((Page)Activator.CreateInstance(typeof(FigurjaktCreate)));
            Navigation.PushAsync(newPage);
        }
    }
}

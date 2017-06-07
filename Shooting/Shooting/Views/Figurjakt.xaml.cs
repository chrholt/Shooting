using Shooting.Database;
using Shooting.Views;
using Shooting.ViewsDetails;
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
            
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "plus_round_128x128_white_hollow.png",
                Text = "New Result",
                Command = new Command(this.GoToRegisterFigurjaktResult)
                
            });

            
        }

        private void figurjaktResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (Result)e.SelectedItem;
            var newPage = new FigurjaktDetails(selectedItem);
            newPage.BindingContext = selectedItem;
            Navigation.PushAsync(newPage);
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

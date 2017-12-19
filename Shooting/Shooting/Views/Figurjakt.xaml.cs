//using Shooting.Database;
using Newtonsoft.Json;
using Shooting.Views;
using Shooting.ViewsDetails;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Figurjakt : ContentPage
    {
        //private ShootingDatabase database;
        private ObservableCollection<Result> figurjaktResults = new ObservableCollection<Result>();
        
        public Figurjakt()
        {
            InitializeComponent();
            //this.database = new ShootingDatabase();
            
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "plus_round_128x128_white_hollow.png",
                Text = "New Result",
                Command = new Command(this.GoToRegisterFigurjaktResult)
                
            });
            //figurjaktResults = database.GetFigurjaktResults();
            GetFigurjaktResults();
            figurjaktResultsListView.ItemsSource = figurjaktResults;
        }

        private void figurjaktResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (Result)e.SelectedItem;
            var newPage = new FigurjaktDetails(selectedItem, figurjaktResults);
            newPage.BindingContext = selectedItem;
            Navigation.PushAsync(newPage);
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
            
            
        //}

        private void GoToRegisterFigurjaktResult()
        {
            var newPage = new FigurjaktCreate(figurjaktResults);
            Navigation.PushAsync(newPage);
        }


        private void figurjaktResultsListViewSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = figurjaktResultsListViewSearchBar.Text;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                figurjaktResultsListView.ItemsSource = from f in figurjaktResults
                                                       where f.Name.Contains(keyword) || f.Date.ToString("dd/MM/yyyy").Contains(keyword)
                                                       select f;
            }
                //figurjaktResults.Where(name => name.Name.Contains(keyword));
            //figurjaktResults.Where(date => date.Date.ToString().Contains(keyword)));
        }

        public async void GetFigurjaktResults()
        {
            try { 
                using (var client = new HttpClient())
                {
                    //SHOW ACTIVITYINDICATOR AND START ANIMATION
                    activityIndicatorFJResultsListView.IsVisible = true;
                    activityIndicatorFJResultsListView.IsRunning = true;
                    client.BaseAddress = new Uri("http://shootingwebapi.azurewebsites.net/");
                    using (var r = await client.GetAsync("api/results/GetFigurjaktResults"))
                    {
                        var result = await r.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<List<Result>>(result);
                        foreach (var re in res)
                        {
                            figurjaktResults.Add(re);
                        }
                    }
                    //STOP ANIMATION AND HIDE ACTIVITYINDICATOR
                    activityIndicatorFJResultsListView.IsRunning = false;
                    activityIndicatorFJResultsListView.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(ex.GetBaseException().Message, ex.InnerException.Message, "OK");
            }

        }
    }
}

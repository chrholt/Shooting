//using Shooting.Database;
using Newtonsoft.Json;
using Shooting.ViewsCreate;
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
    public partial class Jegertrap : ContentPage
    {
        //private ShootingDatabase database;
        private ObservableCollection<Result> jegertrapResults = new ObservableCollection<Result>();

        public Jegertrap()
        {
            InitializeComponent();
            //this.database = new ShootingDatabase();

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "plus_round_128x128_white_hollow.png",
                Text = "New Result",
                Command = new Command(this.GoToRegisterJegertrapResult)

            });
            //jegertrapResults = database.GetJegertrapResults();
            GetJegertrapResults();
            jegertrapResultsListView.ItemsSource = jegertrapResults;
        }

        private void jegertrapResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (Result)e.SelectedItem;
            var newPage = new JegertrapDetails(selectedItem, jegertrapResults);
            newPage.BindingContext = selectedItem;
            Navigation.PushAsync(newPage);
        }

        private void GoToRegisterJegertrapResult()
        {
            var newPage = new JegertrapCreate(jegertrapResults);
            Navigation.PushAsync(newPage);
        }

        private void jegertrapResultsListViewSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = jegertrapResultsListViewSearchBar.Text;
            if (!String.IsNullOrWhiteSpace(keyword))
                {
                    jegertrapResultsListView.ItemsSource = from f in jegertrapResults
                                                           where f.Name.Contains(keyword) || f.Date.ToString("dd/MM/yyyy").Contains(keyword)
                                                           select f;
                }
            //figurjaktResults.Where(name => name.Name.Contains(keyword));
            //figurjaktResults.Where(date => date.Date.ToString().Contains(keyword)));
        }

        public async void GetJegertrapResults()
        {
            try { 
                using (var client = new HttpClient())
                {
                    //SHOW ACTIVITYINDICATOR AND START ANIMATION
                    activityIndicatorJTResultsListView.IsVisible = true;
                    activityIndicatorJTResultsListView.IsRunning = true;
                    client.BaseAddress = new Uri("http://shootingwebapi.azurewebsites.net/");
                    using (var r = await client.GetAsync("api/results/GetJegertrapResults"))
                    {
                        var result = await r.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<List<Result>>(result);
                        foreach (var re in res)
                        {
                            jegertrapResults.Add(re);
                        }
                    }
                    //STOP ANIMATION AND HIDE ACTIVITYINDICATOR
                    activityIndicatorJTResultsListView.IsRunning = false;
                    activityIndicatorJTResultsListView.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(ex.GetBaseException().Message, ex.InnerException.Message, "OK");
            }

        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //}
    }
}

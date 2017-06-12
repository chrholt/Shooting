using Newtonsoft.Json;
using Shooting.Database;
using Shooting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting.ViewsDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JegertrapDetails : ContentPage
    {
        private ShootingDatabase database;
        private ObservableCollection<Result> jegertrapResults;
        private ObservableCollection<JegertrapSeries> jegertrapResultSeries;
        private Result result;

        public JegertrapDetails(Result result, ObservableCollection<Result> jegertrapResults)
        {
            InitializeComponent();
            this.result = result;
            this.jegertrapResults = jegertrapResults;
            
            //SET CONTENT
            JegertrapResult res = JsonConvert.DeserializeObject<JegertrapResult>(result.Results);
            var hits = res.AchievedPoints;

            jegertrapResultSeries = res.Series;
            jegertrapResultSeriesListView.ItemsSource = jegertrapResultSeries;
            achievablePointsDetailsLabel.Text = res.AchievablePoints.ToString();
            achievedPointsDetailsLabel.Text = res.AchievedPoints.ToString();


            //ADD TOOLBARITEM
            //DELETE
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "delete_32x32_white.png",
                Text = "Delete Result",
                Command = new Command(this.DeleteResult)

            });
        }

        private async void DeleteResult()
        {
            database = new ShootingDatabase();

            var answer = await DisplayAlert("Delete result?", "Are you sure you want to delete?", "Yes", "No");
            if (answer)
            {
                jegertrapResults.Remove(result);
                database.DeleteResult(result);
                await Navigation.PopAsync();
            }

        }
    }
}

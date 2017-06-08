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
    public partial class FigurjaktDetails : ContentPage
    {
        private ShootingDatabase database;
        private ObservableCollection<Result> figurjaktResults;
        private Result result;
        public FigurjaktDetails(Result result, ObservableCollection<Result> figurjaktResults)
        {
            InitializeComponent();
            this.result = result;
            this.figurjaktResults = figurjaktResults;

            //SET CONTENT
            FigurjaktResult res = JsonConvert.DeserializeObject<FigurjaktResult>(result.Results);

            achievablePointsDetailsLabel.Text = res.AchievablePoints.ToString();
            achievedPointsDetailsLabel.Text = res.AchievedPoints.ToString();

            var percentage = (res.AchievedPoints * 100) / res.AchievablePoints;
            string qualificationMark;
            string qualImage = "";

            //CONDITIONAL CONTENT BASED ON RESULT
            if (percentage >= 88) { qualificationMark = "GULL"; qualImage = "gold_128x128.png"; }
            else if (percentage >= 75) { qualificationMark = "SØLV"; qualImage = "silver_128x128.png"; }
            else if(percentage>= 60) { qualificationMark = "BRONSE"; qualImage = "bronze_128x128.png"; }
            else { qualificationMark = ""; }
            if (String.IsNullOrEmpty(qualificationMark))
            {
            qualificationLabel.Text = String.Format("I denne konkurransen ble {0} % av maksimal poengsum oppnådd. Det betyr at du dessverre ikke klarte å oppnå NJFF's minstekrav for noe ferdighetsmerke.", percentage);
            }
            else
            {
            qualificationLabel.Text = String.Format("I denne konkurransen ble {0} % av maksimal poengsum oppnådd. Det betyr at du oppnådde NJFF's minstekrav for ferdighetsmerket {1}.", percentage, qualificationMark);
            }
            qualificationImage.Source = ImageSource.FromFile(qualImage);

            //ADD TOOLBARITEM
            //DELETE
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "delete_32x32.png",
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
                figurjaktResults.Remove(result);
                database.DeleteResult(result);
                await Navigation.PopAsync();
            }
            
        }

    }
}

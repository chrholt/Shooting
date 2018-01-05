using Newtonsoft.Json;
using Shooting.Database;
using Shooting.Models;
using Shooting.ViewsEdit;
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

            //SetContent();

            //ADD TOOLBARITEMS
            //DELETE
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "delete_32x32_white.png",
                Text = "Delete Result",
                Command = new Command(this.DeleteResult)

            });
            //EDIT
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "edit_128x128_white.png",
                Text = "Edit Result",
                Command = new Command(this.GoToEditFigurjaktResult)

            });

        }

        private void SetContent()
        {
            database = new ShootingDatabase();
            result = database.GetResult(result.ID);
            BindingContext = result;
            //SET CONTENT
            FigurjaktResult res = JsonConvert.DeserializeObject<FigurjaktResult>(result.Results);
            var percentage = (res.AchievedPoints * 100) / res.AchievablePoints;

            achievablePointsDetailsLabel.Text = res.AchievablePoints.ToString();
            achievedPointsDetailsLabel.Text = res.AchievedPoints.ToString();

            percentageLabel.Text = percentage.ToString() + "%";


            string qualificationMark;
            string qualImage = "";

            //CONDITIONAL CONTENT BASED ON RESULT
            if (percentage >= 88) { qualificationMark = "GULL"; qualImage = "gold_128x128.png"; }
            else if (percentage >= 75) { qualificationMark = "SØLV"; qualImage = "silver_128x128.png"; }
            else if (percentage >= 60) { qualificationMark = "BRONSE"; qualImage = "bronze_128x128.png"; }
            else { qualificationMark = ""; }
            if (String.IsNullOrEmpty(qualificationMark))
            {
                qualificationLabel.Text = "";
            }
            else
            {
                qualificationLabel.Text = String.Format("I denne konkurransen oppfylte du NJFF's minstekrav for ferdighetsmerket {0}.", qualificationMark);
            }
            qualificationImage.Source = ImageSource.FromFile(qualImage);
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

        private async void GoToEditFigurjaktResult()
        {
            var newPage = new FigurjaktEdit(result);
            await Navigation.PushAsync(newPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetContent();
        }

    }
}

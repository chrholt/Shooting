using Newtonsoft.Json;
//using Shooting.Database;
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
    public partial class JaktfeltDetails : ContentPage
    {
        //private ShootingDatabase database;
        private ObservableCollection<Result> jaktfeltResults;
        private ObservableCollection<JaktfeltPost> jaktfeltResultPosts;
        private Result result;

        public JaktfeltDetails(Result result, ObservableCollection<Result> jaktfeltResults)
        {
            InitializeComponent();
            this.result = result;
            this.jaktfeltResults = jaktfeltResults;

            ////SET CONTENT
            JaktfeltResult res = JsonConvert.DeserializeObject<JaktfeltResult>(result.Results);
            var hits = res.Hits;
            var InnerHits = res.InnerHits;
            var points = ((hits * 3) + (InnerHits * 2));

            achievablePointsDetailsLabel.Text = "150";
            achievedPointsDetailsLabel.Text = (points).ToString();

            jaktfeltResultPosts = res.Posts;
            jaktfeltResultPostsListView.ItemsSource = jaktfeltResultPosts;

            string qualImage="", qualMark;
            if (points >= 144) { qualImage = "gold_128x128.png"; qualMark = "GULL"; }
            else if (points >= 136) { qualImage = "silver_128x128.png"; qualMark = "SØLV"; }
            else if (points >= 126) { qualImage = "bronze_128x128.png"; qualMark = "BRONSE"; }
            else { qualMark = ""; }

            if (String.IsNullOrWhiteSpace(qualMark))
            {
                qualificationLabel.Text = "";
            }
            else
            {
                qualificationLabel.Text = String.Format("I denne konkurransen oppfylte du NJFF's minstekrav for ferdighetsmerket {0}.", qualMark);
                qualificationImage.Source = ImageSource.FromFile(qualImage);
            }

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
            //database = new ShootingDatabase();

            var answer = await DisplayAlert("Delete result?", "Are you sure you want to delete?", "Yes", "No");
            if (answer)
            {
                jaktfeltResults.Remove(result);
                //database.DeleteResult(result);
                await Navigation.PopAsync();
            }

        }
        ////SET POSTS CONTENT
        //post1Hits.Text = res.P1Hits;
        //post1InnerHits.Text = res.P1InnerHits;

        //post2Hits.Text = res.P2Hits;
        //post2InnerHits.Text = res.P2InnerHits;

        //post3Hits.Text = res.P3Hits;
        //post3InnerHits.Text = res.P3InnerHits;

        //post4Hits.Text = res.P4Hits;
        //post4InnerHits.Text = res.P4InnerHits;

        //post5Hits.Text = res.P5Hits;
        //post5InnerHits.Text = res.P5InnerHits;

        //post6Hits.Text = res.P6Hits;
        //post6InnerHits.Text = res.P6InnerHits;
    
    }
}

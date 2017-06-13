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
    public partial class JaktfeltDetails : ContentPage
    {
        private ShootingDatabase database;
        private ObservableCollection<Result> jaktfeltResults;
        private Result result;

        public JaktfeltDetails(Result result, ObservableCollection<Result> jaktfeltResults)
        {
            InitializeComponent();
            this.result = result;
            this.jaktfeltResults = jaktfeltResults;

            //SET CONTENT
            JaktfeltResult res = JsonConvert.DeserializeObject<JaktfeltResult>(result.Results);
            var hits = res.Hits;
            var InnerHits = res.InnerHits;

            achievablePointsDetailsLabel.Text = "150";
            achievedPointsDetailsLabel.Text = (hits * 3 + InnerHits * 2).ToString();

            //SET POSTS CONTENT
            post1Hits.Text = res.P1Hits;
            post1InnerHits.Text = res.P1InnerHits;

            post2Hits.Text = res.P2Hits;
            post2InnerHits.Text = res.P2InnerHits;

            post3Hits.Text = res.P3Hits;
            post3InnerHits.Text = res.P3InnerHits;

            post4Hits.Text = res.P4Hits;
            post4InnerHits.Text = res.P4InnerHits;

            post5Hits.Text = res.P5Hits;
            post5InnerHits.Text = res.P5InnerHits;

            post6Hits.Text = res.P6Hits;
            post6InnerHits.Text = res.P6InnerHits;
        }
    }
}

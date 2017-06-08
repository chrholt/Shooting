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

namespace Shooting.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FigurjaktCreate : ContentPage
    {
        private ShootingDatabase database;
        private ObservableCollection<Result> oc;
        public FigurjaktCreate()
        {
            InitializeComponent();
            this.database = new ShootingDatabase();
        }
        public FigurjaktCreate(ObservableCollection<Result> figurjaktResults)
        {
            InitializeComponent();
            this.oc = figurjaktResults;
            this.database = new ShootingDatabase();
        }

        private void Save_Result(object sender, EventArgs e)
        {
            Result result = new Result
            {
                Date = datePicker.Date,
                Name = nameEntry.Text,
                StevneID = stevneIDEntry.Text,
                Type = "Figurjakt",
                Results = JsonConvert.SerializeObject(new FigurjaktResult
                {
                    AchievablePoints = Convert.ToInt32(achievablePoints.Text),
                    AchievedPoints = Convert.ToInt32(achievedPoints.Text)
                })
            };
            oc.Add(result);
            database.SaveResult(result);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}

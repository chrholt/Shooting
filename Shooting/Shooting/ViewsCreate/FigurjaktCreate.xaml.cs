using Newtonsoft.Json;
//using Shooting.Database;
using Shooting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FigurjaktCreate : ContentPage
    {
        private const string digitRegex = @"^[0-9]+$";
        //private ShootingDatabase database;
        private ObservableCollection<Result> oc;
        public FigurjaktCreate()
        {
            InitializeComponent();
            //this.database = new ShootingDatabase();

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "save_128x128_white_hollow.png",
                Text = "Save",
                Command = new Command(this.Save_Result)
            });
        }
        public FigurjaktCreate(ObservableCollection<Result> figurjaktResults)
        {
            InitializeComponent();
            this.oc = figurjaktResults;
            //this.database = new ShootingDatabase();

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "save_128x128_white_hollow.png",
                Text = "Save",
                Command = new Command(this.Save_Result)
            });
        }

        private void Save_Result()
        {
            bool pointsAchievedValid = false;
            bool pointsAchievableValid = false;
            bool nameOK = false;
            //VALIDATION OF NAME ENTRY
            if (String.IsNullOrWhiteSpace(nameEntry.Text))
            {
                nameErrorLabel.IsVisible = true;
            }
            else
            {
                nameOK = true;
                nameErrorLabel.IsVisible = false;
            }
            //END - VALIDATION OF NAME ENTRY

            //VALIDATION OF POINTS ACHIEVABLE ENTRY
            if (!Regex.IsMatch(achievablePoints.Text, digitRegex))
            {
                achievablePointsErrorLabel.IsVisible = true;
            }
            else
            {
                pointsAchievableValid = true;
                achievablePointsErrorLabel.IsVisible = false;

            }
            //END - VALIDATION OF POINTS ACHIEVABLE ENTRY

            //VALIDATION OF POINTS ACHIEBED ENTRY
            if (!Regex.IsMatch(achievedPoints.Text, digitRegex))
            {
                achievedPointsErrorLabel.IsVisible = true;
            }
            else
            {
                pointsAchievedValid = true;
                achievedPointsErrorLabel.IsVisible = false;

            }
            //END - VALIDATION OF POINTS ACHIEBED ENTRY

            if (nameOK && pointsAchievedValid && pointsAchievableValid)
            {
                Result result = new Result
                {
                    ID = Guid.NewGuid().ToString(),
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
                //database.SaveResult(result);
                Save_Result(result);
                Navigation.PopAsync();
            }
            else
            {
                
            }
        }

        private async void Save_Result(Result result)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://shootingwebapi.azurewebsites.net/");
                HttpContent content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json");

                HttpResponseMessage x = await client.PostAsync("api/Results/", content);
                x.EnsureSuccessStatusCode();
                System.Diagnostics.Debug.WriteLine(x.StatusCode);
            }

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}

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
//using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting.ViewsCreate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JegertrapCreate : ContentPage
    {
        private const string digitRegex = @"^[0-9]+$";
        //private ShootingDatabase database;
        private ObservableCollection<Result> oc;
        private int rows, series;
        private List<bool> entriesValid;
        public JegertrapCreate()
        {
            InitializeComponent();
            //this.database = new ShootingDatabase();

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "save_128x128_white_hollow.png",
                Text = "Save",
                Command = new Command(this.Save_Result)
            });

            rows = seriesGrid.RowDefinitions.Count;
            series = 1;
            checkerText.Text = series + " serier";
        }

        public JegertrapCreate(ObservableCollection<Result> jegertrapResults)
        {
            InitializeComponent();
            this.oc = jegertrapResults;
            //this.database = new ShootingDatabase();

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "save_128x128_white_hollow.png",
                Text = "Save",
                Command = new Command(this.Save_Result)
            });

            rows = seriesGrid.RowDefinitions.Count;
            series = 1;
            checkerText.Text = series + " serier";

            var behavior = new DigitBehavior();
            behavior.SetBinding(DigitBehavior.IsValidProperty, "IsValid");
            achievedPoints.Behaviors.Add(behavior);
            achievedPointsErrorLabel.SetBinding(Label.IsVisibleProperty, new Binding("IsValid", source: behavior));
            achievedPointsErrorLabel.Text = "Ugyldig - kun heltall";
        }

        private void Save_Result()
        {
            bool pointsAchievedValid = false;
            bool nameOK = false;
            int totalHits=0;
            ObservableCollection<JegertrapSeries> jegertrapSeries = new ObservableCollection<JegertrapSeries>();

            //GET ALL ENTRIES FOR SERIES FROM GRID                 
            var entries = seriesGrid.Children.OfType<Entry>().Where(r => r.ClassId == "99");

                      
                        
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

            //VALIDATION OF POINTS ACHIEBED ENTRY
            entriesValid = new List<bool>();
            var seriesNumber = 0;
            foreach (var i in entries)
            {
                seriesNumber++;
                
                var ok = Regex.IsMatch(i.Text, digitRegex);
                string medal, icon;                
                if (ok)
                {
                    var hits = Convert.ToInt32(i.Text);

                    totalHits += hits;

                    if (hits >= 24) { icon = "gold_16x16.png"; medal = "Gull"; }
                    else if (hits >= 21) { icon = "silver_16x16.png"; medal = "Sølv"; }
                    else if (hits >= 17) { icon = "bronze_16x16.png"; medal = "Bronse"; }
                    else { icon = ""; medal = ""; }

                    jegertrapSeries.Add(new JegertrapSeries
                    {
                        AchievablePoints = 25,
                        AchievedPoints = Convert.ToInt32(i.Text),
                        SeriesName = "Serie "+seriesNumber,
                        Icon = icon,
                        Medal = medal
                    });
                    entriesValid.Add(true);
                }
                else { entriesValid.Add(false); }
            }

            //CHECK ALL VALUES IF THEY ARE VALID
            pointsAchievedValid = AreAllTrue(entriesValid);

            //END - VALIDATION OF POINTS ACHIEBED ENTRY

            if (!nameOK)
            {
                DisplayAlert("Navn", "Navn kan ikke være tomt", "OK");

            }
            else if(!pointsAchievedValid)
            {
                DisplayAlert("Treff", "Angi treff med kun heltall", "OK");
            }
            else if (nameOK && pointsAchievedValid)
            {
                Result result = new Result
                {
                    ID = Guid.NewGuid().ToString(),
                    Date = datePicker.Date,
                    Name = nameEntry.Text,
                    StevneID = stevneIDEntry.Text,
                    Type = "Jegertrap",
                    Results = JsonConvert.SerializeObject(new JegertrapResult
                    {
                        AchievablePoints = series * 25,
                        AchievedPoints = totalHits,
                        Series = jegertrapSeries
                    })
                };
                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(result));
                oc.Add(result);
                //database.SaveResult(result);
                Save_Result(result);
                Navigation.PopAsync();
            }
            else { }
        }

        private async void Save_Result(Result result)
        {
            
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://shootingwebapi.azurewebsites.net/");
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json");

                    HttpResponseMessage x = await client.PostAsync("api/Results/", content);
                //if (!x.IsSuccessStatusCode)
                //{
                //    await DisplayAlert(x.StatusCode.ToString(), x.Headers.Location.Query, "OK");
                //}
               
                }
           

        }

        private void AddSeries(object sender, EventArgs args)
        {
            series++;
            //FIND GRID AND ADD ROW
            Grid grid = seriesGrid;
            rows = seriesGrid.RowDefinitions.Count;
            seriesGrid.RowDefinitions.Add(new RowDefinition());
            seriesGrid.RowDefinitions.Add(new RowDefinition());
            seriesGrid.RowDefinitions.Add(new RowDefinition());


            //COLUMN 0
            var labelAchieved = new Label
            {
                Text = "Treff oppnådd"

            };

            var entryAchieved = new Entry
            {
                ClassId = "99"
            };
            var behavior = new DigitBehavior();
            behavior.SetBinding(DigitBehavior.IsValidProperty, "IsValid");
            entryAchieved.Behaviors.Add(behavior);
            var entryAchievedErrorLabel = new Label
            {
                FontSize = 12,
                IsVisible = false,
                TextColor = Color.Red,
                Text = "Ugyldig - kun heltall"
            };
            entryAchievedErrorLabel.SetBinding(Label.IsVisibleProperty,new Binding("IsValid", source: behavior));

            //COLUMN 1
            var labelAchievable = new Label
            {
                Text = "Maks treff"
            };
            var entryAchievable = new Entry
            {
                IsEnabled = false,
                Text = "25"
            };
            entryAchievable.Behaviors.Add(new DigitBehavior());
            var entryAchievableErrorLabel = new Label
            {
                FontSize = (double)NamedSize.Micro,
                IsVisible = false,
                TextColor = Color.Red,
                Text = "Ugyldig - kun heltall"
            };

            //ADD TO GRID
            seriesGrid.Children.Add(labelAchieved);
            Grid.SetRow(labelAchieved, rows);
            Grid.SetColumn(labelAchieved, 0);

            seriesGrid.Children.Add(labelAchievable);
            Grid.SetRow(labelAchievable, rows);
            Grid.SetColumn(labelAchievable, 1);


            rows++;

            seriesGrid.Children.Add(entryAchieved);
            Grid.SetRow(entryAchieved, rows);
            Grid.SetColumn(entryAchieved, 0);

            seriesGrid.Children.Add(entryAchievable);
            Grid.SetColumn(entryAchievable, 1);
            Grid.SetRow(entryAchievable, rows);

            rows++;

            seriesGrid.Children.Add(entryAchievedErrorLabel);
            Grid.SetRow(entryAchievedErrorLabel, rows);
            Grid.SetColumn(entryAchievedErrorLabel, 0);

            seriesGrid.Children.Add(entryAchievableErrorLabel);
            Grid.SetRow(entryAchievableErrorLabel, rows);
            Grid.SetColumn(entryAchievableErrorLabel, 1);

            

            
            checkerText.Text = series + " serier";
        }

        private void RemoveSeries(object sender, EventArgs args)
        {
            var children = seriesGrid.Children.Count();
            series--;
            //FIND GRID AND DELETE ROW
            Grid grid = seriesGrid;
            rows = seriesGrid.RowDefinitions.Count;


            seriesGrid.RowDefinitions.RemoveAt(rows-1);

            seriesGrid.Children.RemoveAt(children - 1);
            seriesGrid.Children.RemoveAt(children - 2);

            seriesGrid.RowDefinitions.RemoveAt(rows-2);

            seriesGrid.Children.RemoveAt(children - 3);
            seriesGrid.Children.RemoveAt(children - 4);

            seriesGrid.RowDefinitions.RemoveAt(rows-3);

            seriesGrid.Children.RemoveAt(children - 5);
            seriesGrid.Children.RemoveAt(children - 6);

            checkerText.Text = series + " serier";


        }

        public static bool AreAllTrue(List<bool> values)
        {
            return values.All(v => v == true);
        }
    }
}

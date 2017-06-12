using Newtonsoft.Json;
using Shooting.Database;
using Shooting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting.ViewsCreate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JegertrapCreate : ContentPage
    {
        private const string digitRegex = @"^[0-9]+$";
        private ShootingDatabase database;
        private ObservableCollection<Result> oc;
        private int rows, series;
        private List<bool> entriesValid;
        public JegertrapCreate()
        {
            InitializeComponent();
            this.database = new ShootingDatabase();

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
            this.database = new ShootingDatabase();

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
            foreach(var i in entries)
            {
                var ok = Regex.IsMatch(i.Text, digitRegex);
                if (ok)
                {
                    totalHits += Convert.ToInt32(i.Text);
                    jegertrapSeries.Add(new JegertrapSeries
                    {
                        AchievablePoints = 25,
                        AchievedPoints = Convert.ToInt32(i.Text),
                        SeriesName = "Serie "+series
                    });
                    entriesValid.Add(true);
                }
                else { entriesValid.Add(false); }
            }

            //CHECK ALL VALUES IF THEY ARE VALID
            pointsAchievedValid = AreAllTrue(entriesValid);

            //END - VALIDATION OF POINTS ACHIEBED ENTRY

            if (nameOK && pointsAchievedValid)
            {
                Result result = new Result
                {
                    Date = datePicker.Date,
                    Name = nameEntry.Text,
                    StevneID = stevneIDEntry.Text,
                    Type = "Jegertrap",
                    Results = JsonConvert.SerializeObject(new JegertrapResult
                    {
                        AchievablePoints = series*25,
                        AchievedPoints = totalHits,
                        Series = jegertrapSeries
                    })
                };
                oc.Add(result);
                database.SaveResult(result);
                Navigation.PopAsync();
            }
            else if(!pointsAchievedValid)
            {
                DisplayAlert("ERROR", "Angi treff med kun heltall", "OK");
            }
            else { }
        }

        private void AddSeries(object sender, EventArgs args)
        {
            //FIND GRID AND ADD ROW
            Grid grid = seriesGrid;
            rows = seriesGrid.RowDefinitions.Count;
            series = rows / 3;
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

            

            series++;
            checkerText.Text = series + " serier";
            var elements = seriesGrid.Children.OfType<Entry>().Where(r => r.ClassId == "99");
            var count = elements.Count();
            var a = 1 + 2;
        }

        public static bool AreAllTrue(List<bool> values)
        {
            return values.All(v => v == true);
        }
    }
}

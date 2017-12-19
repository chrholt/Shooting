using Newtonsoft.Json;
//using Shooting.Database;
using Shooting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Shooting.ViewsCreate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JaktfeltCreate : ContentPage
    {
        private const string digitRegex = @"^[0-9]+$";
        //private ShootingDatabase database;
        private ObservableCollection<Result> oc;
        private List<bool> entriesValid;

        public JaktfeltCreate()
        {
            InitializeComponent();
            //this.database = new ShootingDatabase();
            //this.oc = database.GetJaktfeltResults();

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "save_128x128_white_hollow.png",
                Text = "Save",
                Command = new Command(this.Save_Result)
            });

            //LIST OF ERROR LABELS
            var errorLabels = new List<Label>();
            //LIST OF HITS ENTRIES
            var hitsEntries = new List<Entry>();
            //ADD ERROR TO THE LIST OF ERROR LABELS FOR ADDING BEHAVIOR TO THEM
            errorLabels.Add(post1HitsErrorLabel);
            errorLabels.Add(post1InnerHitsErrorLabel);
            errorLabels.Add(post2HitsErrorLabel);
            errorLabels.Add(post2InnerHitsErrorLabel);
            errorLabels.Add(post3HitsErrorLabel);
            errorLabels.Add(post3InnerHitsErrorLabel);
            errorLabels.Add(post4HitsErrorLabel);
            errorLabels.Add(post4InnerHitsErrorLabel);
            errorLabels.Add(post5HitsErrorLabel);
            errorLabels.Add(post5InnerHitsErrorLabel);
            errorLabels.Add(post6HitsErrorLabel);
            errorLabels.Add(post6InnerHitsErrorLabel);

            foreach (var l in errorLabels)
            {
                //ADD BEHAVIOR TO LABELS
                var behavior = new ValueBehavior();
                behavior.SetBinding(ValueBehavior.IsValidProperty, "IsValid");
                l.SetBinding(Label.IsVisibleProperty, new Binding("IsValid", source: behavior));
            }
            //ADD ENTRIES TO THE LIST OF HITS ENTRIES FOR ADDING BEHAVIOR TO THEM
            hitsEntries.Add(post1Hits);
            hitsEntries.Add(post1InnerHits);
            hitsEntries.Add(post2Hits);
            hitsEntries.Add(post2InnerHits);
            hitsEntries.Add(post3Hits);
            hitsEntries.Add(post3InnerHits);
            hitsEntries.Add(post4Hits);
            hitsEntries.Add(post4InnerHits);
            hitsEntries.Add(post5Hits);
            hitsEntries.Add(post5InnerHits);
            hitsEntries.Add(post6Hits);
            hitsEntries.Add(post6InnerHits);

            //ADD BEHAVIOR TO ALL HITS ENTRIES
            foreach (var e in hitsEntries)
            {
                var behavior = new ValueBehavior();
                behavior.SetBinding(ValueBehavior.IsValidProperty, "IsValid");
                e.Behaviors.Add(behavior);
            }
        }

        public JaktfeltCreate(ObservableCollection<Result> jaktfeltResults)
        {
            InitializeComponent();
            if(jaktfeltResults == null)
            {               
                this.oc = new ObservableCollection<Result>();
            }
            else { this.oc = jaktfeltResults; }
            //this.database = new ShootingDatabase();

            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "save_128x128_white_hollow.png",
                Text = "Save",
                Command = new Command(this.Save_Result)
            });
            //LIST OF ERROR LABELS
            var errorLabels = new List<Label>();
            //LIST OF HITS ENTRIES
            var hitsEntries = new List<Entry>();
            //ADD ERROR TO THE LIST OF ERROR LABELS FOR ADDING BEHAVIOR TO THEM
            errorLabels.Add(post1HitsErrorLabel);
            errorLabels.Add(post1InnerHitsErrorLabel);
            errorLabels.Add(post2HitsErrorLabel);
            errorLabels.Add(post2InnerHitsErrorLabel);
            errorLabels.Add(post3HitsErrorLabel);
            errorLabels.Add(post3InnerHitsErrorLabel);
            errorLabels.Add(post4HitsErrorLabel);
            errorLabels.Add(post4InnerHitsErrorLabel);
            errorLabels.Add(post5HitsErrorLabel);
            errorLabels.Add(post5InnerHitsErrorLabel);
            errorLabels.Add(post6HitsErrorLabel);
            errorLabels.Add(post6InnerHitsErrorLabel);
            
            foreach(var l in errorLabels)
            {
                //ADD BEHAVIOR TO LABELS
                var behavior = new ValueBehavior();
                behavior.SetBinding(ValueBehavior.IsValidProperty, "IsValid");
                l.SetBinding(Label.IsVisibleProperty, new Binding("IsValid", source: behavior));
            }
            //ADD ENTRIES TO THE LIST OF HITS ENTRIES FOR ADDING BEHAVIOR TO THEM
            hitsEntries.Add(post1Hits);
            hitsEntries.Add(post1InnerHits);
            hitsEntries.Add(post2Hits);
            hitsEntries.Add(post2InnerHits);
            hitsEntries.Add(post3Hits);
            hitsEntries.Add(post3InnerHits);
            hitsEntries.Add(post4Hits);
            hitsEntries.Add(post4InnerHits);
            hitsEntries.Add(post5Hits);
            hitsEntries.Add(post5InnerHits);
            hitsEntries.Add(post6Hits);
            hitsEntries.Add(post6InnerHits);

            //ADD BEHAVIOR TO ALL HITS ENTRIES
            foreach (var e in hitsEntries)
            {
                var behavior = new ValueBehavior();
                behavior.SetBinding(ValueBehavior.IsValidProperty, "IsValid");
                e.Behaviors.Add(behavior);
            }
        }

        private void Save_Result()
        {
            //LIST OF HITS ENTRIES
            var hitsEntries = new List<JaktfeltPost>();

            bool hitsEntriesValid = false;
            bool nameOK = false;
            int hits = 0, innerHits =0;

            ObservableCollection<JaktfeltPost> jaktfeltPosts = new ObservableCollection<JaktfeltPost>();

            //ADD ENTRIES TO THE LIST OF ENTRIES FOR VALIDATION CHECK
            hitsEntries.Add(new JaktfeltPost {
                Hits = Convert.ToInt32(post1Hits.Text),
                InnerHits = Convert.ToInt32(post1InnerHits.Text),
                PostName = "Post 1"
            });

            hitsEntries.Add(new JaktfeltPost
            {
                Hits = Convert.ToInt32(post2Hits.Text),
                InnerHits = Convert.ToInt32(post2InnerHits.Text),
                PostName = "Post 2"
            });

            hitsEntries.Add(new JaktfeltPost
            {
                Hits = Convert.ToInt32(post3Hits.Text),
                InnerHits = Convert.ToInt32(post3InnerHits.Text),
                PostName = "Post 3"
            });

            hitsEntries.Add(new JaktfeltPost
            {
                Hits = Convert.ToInt32(post4Hits.Text),
                InnerHits = Convert.ToInt32(post4InnerHits.Text),
                PostName = "Post 4"
            });

            hitsEntries.Add(new JaktfeltPost
            {
                Hits = Convert.ToInt32(post5Hits.Text),
                InnerHits = Convert.ToInt32(post5InnerHits.Text),
                PostName = "Post 5"
            });

            hitsEntries.Add(new JaktfeltPost
            {
                Hits = Convert.ToInt32(post6Hits.Text),
                InnerHits = Convert.ToInt32(post6InnerHits.Text),
                PostName = "Post 6"
            });

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

            //VALIDATION OF HITS ENTRIES
            entriesValid = new List<bool>();
            foreach(var e in hitsEntries)
            {
                var ok = e.InnerHits <= 5 && e.Hits <= 5;
                if (ok)
                {
                    hits +=  e.Hits;
                    innerHits += e.InnerHits;
                    
                    entriesValid.Add(true);
                    jaktfeltPosts.Add(e);
                }
                else { entriesValid.Add(false); }
            }

            //CHECK IF ALL VALUES ARE VALID
            hitsEntriesValid = AreAllTrue(entriesValid);

            //END - VALIDATION OF HITS ENTRIES

            if (!nameOK)
            {
                DisplayAlert("Navn", "Navn kan ikke være tomt", "OK");
            }
            else if (!hitsEntriesValid)
            {
                DisplayAlert("Treff og innertreff", "Maksimum verdi = 5", "OK");
            }
            else if(nameOK && hitsEntriesValid)
            {
                Result result = new Result
                {
                    ID = Guid.NewGuid().ToString(),
                    Date = datePicker.Date,
                    Name = nameEntry.Text,
                    StevneID = stevneIDEntry.Text,
                    Type = "Jaktfelt",
                    Results = JsonConvert.SerializeObject(new JaktfeltResult
                    {
                        Hits = hits,
                        InnerHits = innerHits,
                        Posts = jaktfeltPosts
                    })
                };
                oc.Add(result);
                //database.SaveResult(result);
                Save_Result(result);
                Navigation.PopAsync();
            }
            else { }

        }

        private async void Save_Result(Result result)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://shootingwebapi.azurewebsites.net/");
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json");
                    HttpResponseMessage x = await client.PostAsync("api/Results/", content);
                }
            }
            catch(Exception ex)
            {

            }

        }

        private bool AreAllTrue(List<bool> entriesValid)
        {
            return entriesValid.All(v=> v == true);
        }
    }
}

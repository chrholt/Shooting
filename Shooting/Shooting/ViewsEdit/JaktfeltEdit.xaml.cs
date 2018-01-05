using Newtonsoft.Json;
using Shooting.Database;
using Shooting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting.ViewsEdit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JaktfeltEdit : ContentPage
    {
        private ShootingDatabase database;
        Result result;
        private JaktfeltEditViewModel jfEditVM;
        public JaktfeltEdit(Result result)
        {
            InitializeComponent();
            this.result = result;
            var jfresults = JsonConvert.DeserializeObject<JaktfeltResult>(result.Results);
            jfEditVM = new JaktfeltEditViewModel
            {
                Result = result,
                JaktfeltResult = jfresults,
                JaktfeltPost1 = (JaktfeltPost)jfresults.Posts.SingleOrDefault(p => p.PostName == "Post 1"),
                JaktfeltPost2 = (JaktfeltPost)jfresults.Posts.SingleOrDefault(p => p.PostName == "Post 2"),
                JaktfeltPost3 = (JaktfeltPost)jfresults.Posts.SingleOrDefault(p => p.PostName == "Post 3"),
                JaktfeltPost4 = (JaktfeltPost)jfresults.Posts.SingleOrDefault(p => p.PostName == "Post 4"),
                JaktfeltPost5 = (JaktfeltPost)jfresults.Posts.SingleOrDefault(p => p.PostName == "Post 5"),
                JaktfeltPost6 = (JaktfeltPost)jfresults.Posts.SingleOrDefault(p => p.PostName == "Post 6")
            };

            BindingContext = jfEditVM;
            //Add toolbar item
            ToolbarItems.Add(new ToolbarItem
            {
                Icon = "save_128x128_white_hollow.png",
                Text = "Save",
                Command = new Command(this.UpdateResult)
            });
        }

        private void UpdateResult()
        {
            if (result != null)
            {
                //Calculate points over again
                jfEditVM.JaktfeltResult.Hits = 0;
                jfEditVM.JaktfeltResult.InnerHits = 0;
                foreach(var p in jfEditVM.JaktfeltResult.Posts)
                {
                    jfEditVM.JaktfeltResult.Hits += p.Hits;
                    jfEditVM.JaktfeltResult.InnerHits += p.InnerHits;
                }
                //END OF CALCULATION
                result.Results = JsonConvert.SerializeObject(jfEditVM.JaktfeltResult);
                database = new ShootingDatabase();
                database.UpdateResult(result);
                DisplayAlert("Updated", "Successfully updated in db", "OK");
                Navigation.PopAsync();
            }
        }
    }

    public class JaktfeltEditViewModel
    {
        public Result Result { get; set; }
        public JaktfeltResult JaktfeltResult { get; set; }
        public JaktfeltPost JaktfeltPost1 { get; set; }
        public JaktfeltPost JaktfeltPost2 { get; set; }
        public JaktfeltPost JaktfeltPost3 { get; set; }
        public JaktfeltPost JaktfeltPost4 { get; set; }
        public JaktfeltPost JaktfeltPost5 { get; set; }
        public JaktfeltPost JaktfeltPost6 { get; set; }
    }
}
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
    public partial class FigurjaktEdit : ContentPage
    {
        private ShootingDatabase database;
        Result result;
        private FigurjaktEditViewModel fjEditVM = new FigurjaktEditViewModel();
        public FigurjaktEdit(Result result)
        {
            InitializeComponent();
            //Setting values to View Model and setting BindingContext
            this.result = result;
            fjEditVM.Result = result;           
            fjEditVM.FigurjaktResult = JsonConvert.DeserializeObject<FigurjaktResult>(result.Results);
            BindingContext = fjEditVM;

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
                result.Results = JsonConvert.SerializeObject(fjEditVM.FigurjaktResult);
                database = new ShootingDatabase();
                database.UpdateResult(result);
                DisplayAlert("Updated", "Successfully updated in db", "OK");
                Navigation.PopAsync();
            }
        }
    }

    public class FigurjaktEditViewModel
    {
        public Result Result { get; set; }
        public FigurjaktResult FigurjaktResult { get; set; }
        
    }
}
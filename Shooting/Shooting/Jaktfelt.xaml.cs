using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Jaktfelt : ContentPage
    {
        public Jaktfelt()
        {
            InitializeComponent();
        }

        private void Register_Jaktfelt(object sender, EventArgs e)
        {
            DisplayAlert("SUCCESS", "Successfully registered results", "GREAT!");
        }
    }
}

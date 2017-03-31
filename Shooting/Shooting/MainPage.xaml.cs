using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shooting
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Jaktfelt_page(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Jaktfelt());
        }
        private async void Figurjakt_page(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Figurjakt());
        }
        private async void Jegertrap_page(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Jegertrap());
        }
    }
}

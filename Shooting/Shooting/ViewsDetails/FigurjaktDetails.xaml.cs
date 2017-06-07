using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting.ViewsDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FigurjaktDetails : ContentPage
    {
        public FigurjaktDetails(Result result)
        {
            InitializeComponent();

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shooting.ViewsDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JaktfeltDetails : ContentPage
    {
        public JaktfeltDetails(Result result, ObservableCollection<Result> jaktfeltResults)
        {
            InitializeComponent();
        }
    }
}

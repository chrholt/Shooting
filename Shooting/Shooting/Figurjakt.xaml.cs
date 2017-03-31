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
    public partial class Figurjakt : ContentPage
    {
        public Figurjakt()
        {
            InitializeComponent();

            regulatePosts(1);
        }

        private void figurjaktSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            //< Label Text = "Post 1" /> 
            //     < StackLayout Orientation = "Horizontal" >
            //          < StackLayout >  
            //              < Label Text = "Shot 1" />   
            //               < Entry Placeholder = "Hits - shot 1" ></ Entry >    
            //            </ StackLayout >
            //            < StackLayout >    
            //                < Label Text = "Shot 2" />   
            //                 < Entry Placeholder = "Hits - shot 2" ></ Entry >    
            //              </ StackLayout >     
            //          </ StackLayout >
            //figurjaktPosts
            var value = Convert.ToInt16(e.NewValue);
            regulatePosts(value);
            
        }
        private void regulatePosts(int numberOfPosts)
        {
            figurjaktPosts.Children.Clear();
            for(int i=1; i <= numberOfPosts; i++) {
                var frame = new Frame();
                //Sframe.HorizontalOptions = LayoutOptions.CenterAndExpand;
                figurjaktPosts.Children.Add(frame);
                var stacklay = new StackLayout();
                stacklay.HorizontalOptions = LayoutOptions.Center;
                frame.Content = stacklay;
                var postLabel = new Label();
                postLabel.Text = "Post " + i;
                postLabel.TextColor = Color.Black;
                postLabel.FontSize = 20;
                var stackLayout1 = new StackLayout();
                stackLayout1.Orientation = StackOrientation.Horizontal;

                stacklay.Children.Add(postLabel);
                stacklay.Children.Add(stackLayout1);

                var innerStackLayout1 = new StackLayout();
                var innerStackLayout1Label = new Label();
                innerStackLayout1Label.Text = "Shot 1";
                innerStackLayout1Label.TextColor = Color.Black;
                var innerStackLayout1Entry = new Entry();
                innerStackLayout1Entry.Placeholder = "Hits - shot 1";
                innerStackLayout1Entry.ClassId = "post"+i+"shot1";
                var labell = new Label();
                labell.Text = innerStackLayout1Entry.ClassId;

                stackLayout1.Children.Add(innerStackLayout1);
                innerStackLayout1.Children.Add(innerStackLayout1Label);
                innerStackLayout1.Children.Add(innerStackLayout1Entry);
                innerStackLayout1.Children.Add(labell);

                

                var innerStackLayout2 = new StackLayout();
                var innerStackLayout2Label = new Label();
                innerStackLayout2Label.Text = "Shot 2";
                innerStackLayout2Label.TextColor = Color.Black;
                var innerStackLayout2Entry = new Entry();
                innerStackLayout2Entry.Placeholder = "Hits - shot 2";
                innerStackLayout2Entry.ClassId = "post" + i + "shot2";
                var label = new Label();
                label.Text = innerStackLayout2Entry.ClassId;

                stackLayout1.Children.Add(innerStackLayout2);
                innerStackLayout2.Children.Add(innerStackLayout2Label);
                innerStackLayout2.Children.Add(innerStackLayout2Entry);
                innerStackLayout2.Children.Add(label);



                var innerStackLayout3 = new StackLayout();
                var innerStackLayout3Label = new Label();
                innerStackLayout3Label.Text = "Single shot";
                innerStackLayout3Label.TextColor = Color.Black;
                var innerStackLayout3Switch = new Switch();
                innerStackLayout3Switch.ClassId = "post" + i + "switch";
                var labelll = new Label();
                labelll.Text = innerStackLayout3Switch.ClassId;
                

                stackLayout1.Children.Add(innerStackLayout3);
                innerStackLayout3.Children.Add(innerStackLayout3Label);
                innerStackLayout3.Children.Add(innerStackLayout3Switch);
                innerStackLayout3.Children.Add(labelll);
                //ACCESS STATIC RESOURCES IN CODE => (Color)Application.Current.Resources["resourceName"]
            }
        }

    }
}

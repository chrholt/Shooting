//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shooting.ViewsDetails {
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    
    public partial class JegertrapDetails : global::Xamarin.Forms.ContentPage {
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Label achievedPointsDetailsLabel;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Label achievablePointsDetailsLabel;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Label SeriesLabel;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ListView jegertrapResultSeriesListView;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(JegertrapDetails));
            achievedPointsDetailsLabel = this.FindByName<global::Xamarin.Forms.Label>("achievedPointsDetailsLabel");
            achievablePointsDetailsLabel = this.FindByName<global::Xamarin.Forms.Label>("achievablePointsDetailsLabel");
            SeriesLabel = this.FindByName<global::Xamarin.Forms.Label>("SeriesLabel");
            jegertrapResultSeriesListView = this.FindByName<global::Xamarin.Forms.ListView>("jegertrapResultSeriesListView");
        }
    }
}

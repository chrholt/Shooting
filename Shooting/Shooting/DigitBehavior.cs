using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shooting
{
    public class DigitBehavior : Behavior<Entry>
    {
        private const string digitRegex = @"^[0-9]+$";
        private const string notEmpty = @"^[a-zA-Z0-9]*$";
        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            "IsValid", 
            typeof(bool), 
            typeof(DigitBehavior),
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get
            {
                return (bool)this.GetValue(IsValidProperty);
            }
            set
            {
                this.SetValue(IsValidPropertyKey, value);
            }
        }
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += bindable_TextChanged;
        }

        private void bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry;
            entry = (Entry)sender;
            this.IsValid = !Regex.IsMatch(e.NewTextValue, digitRegex);

            entry.TextColor = IsValid ? Color.Red : Color.Default;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= this.bindable_TextChanged;
        }
    }
}

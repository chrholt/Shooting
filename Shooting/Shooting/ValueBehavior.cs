using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shooting
{
    public class ValueBehavior : Behavior<Entry>
    {
        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            "IsValid",
            typeof(bool),
            typeof(ValueBehavior),
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
            if (!String.IsNullOrWhiteSpace(entry.Text))
            {
                this.IsValid = Convert.ToInt32(entry.Text) <= 5;

                entry.TextColor = IsValid ? Color.Default : Color.Red;
            }
            
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= this.bindable_TextChanged;
        }
    }
}

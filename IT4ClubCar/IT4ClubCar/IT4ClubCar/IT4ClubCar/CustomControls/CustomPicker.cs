using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.CustomControls
{
    class CustomPicker : Picker
    {
        public static BindableProperty PlaceholderColorProperty =
        BindableProperty.Create("PlaceholderColor", typeof(string), typeof(CustomPicker), "#000000", BindingMode.TwoWay);

        public string PlaceholderColor
        {
            get
            {
                return (string)GetValue(PlaceholderColorProperty);
            }
            set
            {
                SetValue(PlaceholderColorProperty, value);
            }
        }
    }
}

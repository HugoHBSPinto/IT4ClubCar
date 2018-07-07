using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.CustomControls
{
    class CustomPicker : Picker
    {
        public static BindableProperty TextColorProperty =
        BindableProperty.Create("TextColor", typeof(string), typeof(CustomPicker), "#000000", BindingMode.TwoWay);

        public string TextColor
        {
            get
            {
                return (string)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }
    }
}

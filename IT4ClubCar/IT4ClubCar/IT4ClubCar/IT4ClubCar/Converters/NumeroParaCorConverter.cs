﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Converters
{
    class NumeroParaCorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int pontos = 0;

            if (value != null)
                pontos = int.Parse((string)value);

            if (pontos.Equals(6))
                return "#CCCCCC";
            else
                return "#ffffff";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

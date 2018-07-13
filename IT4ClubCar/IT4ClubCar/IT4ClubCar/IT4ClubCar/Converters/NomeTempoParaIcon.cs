using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Converters
{
    class NomeTempoParaIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string nomeTempo = value as string;

            ImageSource icon = null;

            switch(nomeTempo)
            {
                case "Clear":   icon = ImageSource.FromFile("IconClearSky.png");
                                break;
                case "Rain":    icon = ImageSource.FromFile("IconRain.png");
                                break;
                default: icon = ImageSource.FromFile("IconWeatherUndefined.png");
                                break;
            }

            return icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

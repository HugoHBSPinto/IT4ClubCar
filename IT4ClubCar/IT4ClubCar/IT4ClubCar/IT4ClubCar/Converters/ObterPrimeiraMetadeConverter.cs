using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Converters
{
    class ObterPrimeiraMetadeConverter<T> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new ObservableCollection<T>();

            ObservableCollection<T> valores = value as ObservableCollection<T>;

            int metade = valores.Count / 2;

            ObservableCollection<T> primeiraMetadeValores = new ObservableCollection<T>();

            for (int i = 0; i < metade; i++)
                primeiraMetadeValores.Add(valores[i]);

            return primeiraMetadeValores;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Converters
{
    class SomarListaConverter<T> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;

            ObservableCollection<T> dados = value as ObservableCollection<T>;

            string nomePropriedadeASomar = parameter as string;

            int somaTotal = 0;

            foreach(T dado in dados)
            {
                int valorPropriedade = (int)dado.GetType().GetProperty(nomePropriedadeASomar).GetValue(dado);
                somaTotal += valorPropriedade;
            }
            
            return somaTotal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

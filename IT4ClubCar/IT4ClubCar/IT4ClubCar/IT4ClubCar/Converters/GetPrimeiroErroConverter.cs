using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Converters
{
    /// <summary>
    /// Permite obter o primeiro erro de uma lista de erros de um objecto do tipo ValidatableObject.
    /// </summary>
    class GetPrimeiroErroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Value corresponde à coleção de erros. Converte-se para uma Collection<string>
            //de forma a, utilizando Linq, obter o primeiro valor da lista.

            //Converter para uma Collection<string>.
            ICollection<string> mensagensErro = value as ICollection<string>;

            //Se a variável mensagensErro não for null, ou seja está definida, e o seu length é maior do
            //que zero, ou seja tem mensagens de erro, devolve-se a primeiro mensagem. Se não devolve-se
            //null.
            return ((mensagensErro) != null && (mensagensErro.Count > 0)) ? mensagensErro.ElementAt(0) : null;
        }

        //Não utilizado mas teve de ser implementado devido à interface.
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

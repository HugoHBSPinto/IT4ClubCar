using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Base
{
    /// <summary>
    /// Classe que implementa a interface INotifyPropertyChanged, permitindo às classes que herdam desta, avisar da mudança
    /// dos valores de propriedades.
    /// </summary>
    class ExtendedBindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propriedadeNome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedadeNome));
        }
    }
}

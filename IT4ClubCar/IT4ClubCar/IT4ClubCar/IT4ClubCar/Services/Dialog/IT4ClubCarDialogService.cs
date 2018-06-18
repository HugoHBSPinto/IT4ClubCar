using IT4ClubCar.IT4ClubCar.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Services.Dialog
{
    /// <summary>
    /// Classe que implementa a interface IDialogService, oferecendo métodos para mostrar mensagens de vários tipos, 
    /// recorrendo à MainPage da Navegacao.
    /// </summary>
    class IT4ClubCarDialogService : IDialogService
    {
        public async Task MostrarMensagem(string mensagem)
        {
            await Application.Current.MainPage.DisplayAlert("Info", mensagem, "cancel");
        }
    }
}

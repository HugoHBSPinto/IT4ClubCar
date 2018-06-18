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
    /// Classe que implementa a interface IDialogService, oferecendo m�todos para mostrar mensagens de v�rios tipos, 
    /// recorrendo � MainPage da Navegacao.
    /// </summary>
    class IT4ClubCarDialogService : IDialogService
    {
        public async Task MostrarMensagem(string mensagem)
        {
            await Application.Current.MainPage.DisplayAlert("Info", mensagem, "cancel");
        }
    }
}

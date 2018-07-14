using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Jogador
{
    interface IJogadorService
    {
        Task<bool> VerificarSeJogadorExiste(string email,string senha);
        Task<JogadorWrapperViewModel> ObterJogador(string email);
    }
}

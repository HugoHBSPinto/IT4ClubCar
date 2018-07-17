using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.Jogador
{
    interface IJogadorService
    {
        Task<bool> VerificarSeJogadorExisteAsync(string email,string senha);
        Task<JogadorWrapperViewModel> ObterJogadorAsync(string email);
        Task AtualizarDadosJogadorAsync(JogadorWrapperViewModel jogador);
        Task<string> ObterFotoPessoaDefaultAsync();
        Task<bool> VerificarSeEmailEstaEmUso(string emailAVerificar);
        Task<bool> VerificarSeNomeEstaEmUso(string nomeAVerificar);
        Task<int> ObterJogadorUltimoId();
        Task InserirNovoJogador(JogadorWrapperViewModel jogadorAInserir);
    }
}

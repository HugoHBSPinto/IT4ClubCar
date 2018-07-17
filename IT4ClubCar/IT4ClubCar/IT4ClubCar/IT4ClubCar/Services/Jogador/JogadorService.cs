using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using Newtonsoft.Json.Linq;

namespace IT4ClubCar.IT4ClubCar.Services.Jogador
{
    class JogadorService : IJogadorService
    {
        private WebService _webService;



        public JogadorService(WebService webService)
        {
            _webService = webService;
        }



        /// <summary>
        /// Verifica se existe algum jogador associado a um email e senha.
        /// </summary>
        /// <param name="email">Email do jogador.</param>
        /// <param name="senha">Senha do jogador.</param>
        /// <returns>True se o jogador existir,false se não existir.</returns>
        public async Task<bool> VerificarSeJogadorExisteAsync(string email, string senha)
        {
            string dataJson = await _webService.ObterDadosJson("ValidarJogador&Email="+email+"&Senha="+senha);

            JObject dataObject = JObject.Parse(dataJson);

            string existe = dataObject["Existe"].ToString();

            return bool.Parse(existe);
        }


        /// <summary>
        /// Obtém todas as informações do jogador associado a um email.
        /// </summary>
        /// <param name="emailJogador">Email do jogador a obter-se os dados.</param>
        /// <returns>JogadorWrapperViewModel com os dados do jogador associado ao email.</returns>
        public async Task<JogadorWrapperViewModel> ObterJogadorAsync(string emailJogador)
        {
            string dataJson = await _webService.ObterDadosJson("ObterDadosJogador&Email="+ emailJogador);

            JObject dataObject = JObject.Parse(dataJson);

            //Dados Jogador.
            int id = int.Parse(dataObject["Id"].ToString());
            string nome = dataObject["JogadorNome"].ToString();
            string email = emailJogador;
            string fotoBase64 = dataObject["Foto"].ToString();
            int handicap = int.Parse(dataObject["Handicap"].ToString());
            int idGenero = int.Parse(dataObject["IdGenero"].ToString());
            string nomeGenero = dataObject["NomeGenero"].ToString();
            int idTee = int.Parse(dataObject["IdTee"].ToString());
            string nomeTee = dataObject["NomeTee"].ToString();
            
            return new JogadorWrapperViewModel(new JogadorModel(nome,email,new GeneroModel(idGenero,nomeGenero),new HandicapModel(handicap),new TeeModel(idTee,nomeTee),foto : fotoBase64,id : id));
        }



        /// <summary>
        /// Atualiza os dados de um jogador na BD.
        /// </summary>
        /// <param name="jogador">Jogador cujos dados devem ser atualizados.</param>
        public async Task AtualizarDadosJogadorAsync(JogadorWrapperViewModel jogador)
        {
            await _webService.InserirDadosAsync("AtualizarDadosJogador&Id="+jogador.Id+"&Nome="+jogador.Nome+"&Email="+jogador.Email+"&Foto="+jogador.FotoBase64+"&Handicap="+jogador.Handicap.Valor+"&IdGenero="+jogador.Genero.Id+"&IdTee="+jogador.Tee.Id);
        }



        public async Task<string> ObterFotoPessoaDefaultAsync()
        {
            string dataJson = await _webService.ObterDadosJson("GetFotoPessoaDefault");

            JObject dataObject = JObject.Parse(dataJson);

            return dataObject["Foto"].ToString();
        }



        /// <summary>
        /// Verifica se um email já está a ser utilizado por algum jogador.
        /// </summary>
        /// <param name="emailAVerificar">Email a verificar se está a ser usado.</param>
        /// <returns>True se o email estiver a ser usado, false se não estiver.</returns>
        public async Task<bool> VerificarSeEmailEstaEmUso(string emailAVerificar)
        {
            string dataJson = await _webService.ObterDadosJson("VerificarSeEmailEstaEmUso&EmailAVerificar="+emailAVerificar);

            JObject dataObject = JObject.Parse(dataJson);

            return bool.Parse(dataObject["Existe"].ToString());
        }



        /// <summary>
        /// Verifica se um nome já está a ser utilizado por algum jogador.
        /// </summary>
        /// <param name="nomeAVerificar">Nome a verificar se está a ser usado.</param>
        /// <returns>True se o nome estiver a ser usado, false se não estiver.</returns>
        public async Task<bool> VerificarSeNomeEstaEmUso(string nomeAVerificar)
        {
            string dataJson = await _webService.ObterDadosJson("VerificarSeNomeEstaEmUso&NomeAVerificar=" + nomeAVerificar);

            JObject dataObject = JObject.Parse(dataJson);

            return bool.Parse(dataObject["Existe"].ToString());
        }



        /// <summary>
        /// Obtém o último id utilizado por um jogador.
        /// </summary>
        /// <returns>int com o último id utilizado por um jogador.</returns>
        public async Task<int> ObterJogadorUltimoId()
        {
            string dataJson = await _webService.ObterDadosJson("ObterUltimoJogadorId");

            JObject dataObject = JObject.Parse(dataJson);

            int ultimoId = int.Parse(dataObject["Id"].ToString());

            return ultimoId;
        }



        /// <summary>
        /// Insere um novo jogador na BD.
        /// </summary>
        /// <param name="jogadorAInserir">JogadorWrapperViewModel cujas informações devem ser inseridas na BD.</param>
        public async Task InserirNovoJogador(JogadorWrapperViewModel jogadorAInserir)
        {
            await _webService.InserirDadosAsync("InserirNovoJogador&Id=" + jogadorAInserir.Id + "&Nome=" + jogadorAInserir.Nome + "&Email=" + jogadorAInserir.Email + "&Senha=" + jogadorAInserir.Senha + "&Foto="+jogadorAInserir.FotoBase64+"&Handicap=" + jogadorAInserir.Handicap.Valor + "&IdGenero=" + jogadorAInserir.Genero.Id + "&IdTee=" + jogadorAInserir.Tee.Id);
        }

    }
}

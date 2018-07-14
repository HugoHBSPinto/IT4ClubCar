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
        public async Task<bool> VerificarSeJogadorExiste(string email, string senha)
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
        public async Task<JogadorWrapperViewModel> ObterJogador(string emailJogador)
        {
            string dataJson = await _webService.ObterDadosJson("ObterDadosJogador&Email="+ emailJogador);

            JObject dataObject = JObject.Parse(dataJson);

            //Dados Jogador.
            int id = int.Parse(dataObject["Id"].ToString());
            string nome = dataObject["JogadorNome"].ToString();
            string email = emailJogador;
            int handicap = int.Parse(dataObject["Handicap"].ToString());
            int idGenero = int.Parse(dataObject["IdGenero"].ToString());
            string nomeGenero = dataObject["NomeGenero"].ToString();
            int idTee = int.Parse(dataObject["IdTee"].ToString());
            string nomeTee = dataObject["NomeTee"].ToString();

            return new JogadorWrapperViewModel(new JogadorModel(nome,email,new GeneroModel(idGenero,nomeGenero),new HandicapModel(handicap),new TeeModel(idTee,nomeTee)));
        }

    }
}

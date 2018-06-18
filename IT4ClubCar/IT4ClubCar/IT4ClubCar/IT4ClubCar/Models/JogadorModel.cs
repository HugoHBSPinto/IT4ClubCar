using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class JogadorModel
    {
        /// <summary>
        /// Obtém e define o Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém e define o nome.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Obtém e define o Genero.
        /// </summary>
        public GeneroModel Genero { get; set; }

        /// <summary>
        /// Obtém e defnie a FotoBase64.
        /// </summary>
        public string Foto64Base { get; set; }

        /// <summary>
        /// Obtém e define o Handicap.
        /// </summary>
        public HandicapModel Handicap { get; set; }

        /// <summary>
        /// Obtém e define o Tee.
        /// </summary>
        public TeeModel Tee { get; set; }

        /// <summary>
        /// Obtém e define as Pontuacoes.
        /// </summary>
        public List<PontuacaoModel> Pontuacoes { get; set; }



        public JogadorModel()
        {
            
        }
        
        
        
        public JogadorModel(string nome, GeneroModel genero, string fotoBase64, HandicapModel handicap, TeeModel tee)
        {
            Nome = nome;
            Genero = genero;
            Foto64Base = fotoBase64;
            Handicap = handicap;
            Tee = tee;
            Pontuacoes = new List<PontuacaoModel>();
        }



    }
}

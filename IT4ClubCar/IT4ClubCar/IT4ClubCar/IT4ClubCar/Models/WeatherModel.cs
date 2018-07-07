using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class WeatherModel
    {
        /// <summary>
        /// Obtém e define o NomeCidade.
        /// </summary>
        public string NomeCidade { get; set; }

        /// <summary>
        /// Obtém e define o Nome.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Obtém e define a Descricao.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Obtém e define a Temperatura.
        /// </summary>
        public float Temperatura { get; set; }

        /// <summary>
        /// Obtém e define a TemperaturaMaxima.
        /// </summary>
        public float TemperaturaMaxima { get; set; }

        /// <summary>
        /// Obtém e define a TemperaturaMinima.
        /// </summary>
        public float TemperaturaMinima { get; set; }



        public WeatherModel(string nomeCidade,string nome,string descricao,float temperatura,float temperaturaMaxima,float temperaturaMinima)
        {
            NomeCidade = nomeCidade;
            Nome = nome;
            Descricao = descricao;
            Temperatura = temperatura;
            TemperaturaMaxima = temperaturaMaxima;
            TemperaturaMinima = temperaturaMinima;
        }

    }
}

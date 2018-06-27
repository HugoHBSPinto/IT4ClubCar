using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class PublicidadeModel
    {
        /// <summary>
        /// Obtém e define o Titulo.
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Obtém e define a Descricao.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Obtém e define a Imagem.
        /// </summary>
        public ImageSource Imagem { get; set; }



        public PublicidadeModel(string titulo,string descricao,ImageSource imagem)
        {
            Titulo = titulo;
            Descricao = descricao;
            Imagem = imagem;
        }

    }
}

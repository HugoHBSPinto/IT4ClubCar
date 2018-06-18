using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class BuracoModel
    {
        /// <summary>
        /// Obtém e define o Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém e define o Numero.
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Obtém e define o Par.
        /// </summary>
        public int Par { get; set; }

        /// <summary>
        /// Obtém e define o StrokeIndex.
        /// </summary>
        public int StrokeIndex { get; set; }

        /// <summary>
        /// Obtém e define a Dica.
        /// </summary>
        public string Dica { get; set; }

        /// <summary>
        /// Obtém e define as Distancias.
        /// </summary>
        public List<TeeBuracoDistanciaModel> Distancias { get; set; }



        public BuracoModel()
        {
            
        }



        public BuracoModel(int id, int numero, int par, int strokeIndex, string dica)
        {
            Id = id;
            Numero = numero;
            Par = par;
            StrokeIndex = strokeIndex;
            Dica = dica;
            Distancias = new List<TeeBuracoDistanciaModel>();
        }



    }
}

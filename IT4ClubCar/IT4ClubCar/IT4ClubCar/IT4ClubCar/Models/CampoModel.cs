using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class CampoModel
    {
        /// <summary>
        /// Obtém e define o Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém e define o Nome.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Obtém e define o Par.
        /// </summary>
        public int Par { get; set; }

        /// <summary>
        /// Obtém e define o SlopeRating.
        /// </summary>
        public int SlopeRating { get; set; }

        /// <summary>
        /// Obtém e define o NumeroBuracos.
        /// </summary>
        public int NumeroBuracos { get; set; }

        /// <summary>
        /// Obtém e define os Buracos.
        /// </summary>
        public List<BuracoModel> Buracos { get; set; }


        public CampoModel()
        {
            
        }



        public CampoModel(int id, string nome,int par, int slopeRating, int numeroBuracos)
        {
            Id = id;
            Nome = nome;
            Par = par;
            SlopeRating = slopeRating;
            NumeroBuracos = numeroBuracos;
            Buracos = new List<BuracoModel>();
        }



    }
}

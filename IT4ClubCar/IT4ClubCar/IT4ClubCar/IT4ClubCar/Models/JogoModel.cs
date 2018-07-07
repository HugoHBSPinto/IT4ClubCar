using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class JogoModel
    {
        /// <summary>
        /// Obtém e define o Campo.
        /// </summary>
        public CampoModel Campo { get; set; }

        /// <summary>
        /// Obtém e define o ModoJogo.
        /// </summary>
        public ModoJogoModel ModoJogo { get; set; }

        /// <summary>
        /// Obtém e define o Metrico.
        /// </summary>
        public MetricoModel Metrico { get; set; }

        /// <summary>
        /// Obtém e define os Jogadores.
        /// </summary>
        public List<JogadorModel> Jogadores { get; set; }



        public JogoModel()
        {
            
        }



        public JogoModel(CampoModel campo, ModoJogoModel modoJogo, MetricoModel metrico, List<JogadorModel> jogadores)
        {
            Campo = campo;
            ModoJogo = modoJogo;
            Metrico = metrico;
            Jogadores = jogadores;
        }
    }
}

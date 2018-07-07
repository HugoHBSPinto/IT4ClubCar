using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace IT4ClubCar.IT4ClubCar.Services.Geometria
{
    public enum Metrico
    {
        Metro,
        Yard
    }

    interface IGeometriaService
    {
        /// <summary>
        /// Calcula a distância entre dois pontos num mapa.
        /// </summary>
        /// <param name="posicaoOrigem">Posição de partida.</param>
        /// <param name="posicaoDestino">Posição de destino.</param>
        /// <param name="metrico">Tipo de métrico em que a distância deve ser devolvida.</param>
        /// <returns>Double que representa a distância entre os dois pontos.</returns>
        double ObterDistancia(Position posicaoOrigem,Position posicaoDestino,Metrico metrico);
    }
}

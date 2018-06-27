using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Validacoes
{
    /// <summary>
    /// Interface que, quando implementada, permite adicionar um factor de validade a um objecto. O mesmo passa a puder ser
    /// válido ou inválido.
    /// </summary>
    public interface IValidity
    {
        /// <summary>
        /// Obtém e define IsValido.
        /// </summary>
        /// <remarks>Indica se o objecto em que a interface foi implementada é válida ou não tendo 
        /// em conta o valor desta prorpriedade.</remarks>
        bool IsValido { get; set; }
    }
}

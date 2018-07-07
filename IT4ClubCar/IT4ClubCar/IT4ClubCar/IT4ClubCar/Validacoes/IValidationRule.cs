using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Validacoes
{
    /// <summary>
    /// Interface que, quando implementada, permite criar uma regra de validação para ser utilizada com um objecto
    /// do tipo ValidatableObject.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationRule<T>
    {
        /// <summary>
        /// Obtém e define a ValidationMensagem.
        /// </summary>
        /// <remarks>Mensagem a aparecer caso o valor seja inválido.</remarks>
        string ValidationMensagem { get; }

        /// <summary>
        /// Verifica se um determinado valor cumpre esta regra de validação.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Verdadeiro se o value cumprir a regra, false se não cumprir.</returns>
        bool Check(T value);
    }
}

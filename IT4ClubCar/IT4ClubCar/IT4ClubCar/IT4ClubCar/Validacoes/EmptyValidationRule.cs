using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Validacoes
{
    class EmptyValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMensagem => "Não pode estar vazio";

        public bool Check(T value)
        {
            return !String.IsNullOrEmpty(value as string);
        }
    }
}

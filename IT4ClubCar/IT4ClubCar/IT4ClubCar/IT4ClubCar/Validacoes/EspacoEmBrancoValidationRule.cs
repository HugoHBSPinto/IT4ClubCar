using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Validacoes
{
    class EspacoEmBrancoValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMensagem => "Não pode ser um espaço em branco";

        public bool Check(T value)
        {
            return !String.IsNullOrWhiteSpace(value as string);
        }
    }
}

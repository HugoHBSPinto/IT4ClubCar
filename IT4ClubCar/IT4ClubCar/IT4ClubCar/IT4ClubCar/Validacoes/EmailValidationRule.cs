using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Validacoes
{
    class EmailValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMensagem { get => "Email Inválido"; set => throw new NotImplementedException(); }

        public bool Check(T value)
        {
            List<string> terminacoesValidas = new List<string>() { "@gmail.com","@live.com.pt","@outlook.pt", "@aejd.pt" };

            for (int i = 0; i < terminacoesValidas.Count; i++)
                if ((value as string).EndsWith(terminacoesValidas[i]))
                    return true;

            return false;
        }
    }
}

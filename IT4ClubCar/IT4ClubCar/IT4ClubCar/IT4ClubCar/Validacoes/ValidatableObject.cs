using IT4ClubCar.IT4ClubCar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;

namespace IT4ClubCar.IT4ClubCar.Validacoes
{
    class ValidatableObject<T> : ExtendedBindableObject,IValidity
    {
        /// <summary>
        /// Obtém e define as regras de validação a cumprir.
        /// </summary>
        private List<IValidationRule<T>> _regrasValidacao;
        public List<IValidationRule<T>> RegrasValidacao
        {
            get
            {
                return _regrasValidacao;
            }
            set
            {
                _regrasValidacao = value;
            }
        }

        /// <summary>
        /// Obtém e define as mensagens de erro.
        /// </summary>
        /// <remarks>Contém as mensagens de erro das regras não cumpridas após o objecto ter sido validado.</remarks>
        private List<string> _erros;
        public List<string> Erros
        {
            get
            {
                return _erros;
            }
            set
            {
                _erros = value;
                OnPropertyChanged("Erros");
            }
        }

        /// <summary>
        /// Obtém e define o valor.
        /// </summary>
        /// <remarks>É depois utilizado durante a validação para ver se cumpre as regras definidas.</remarks>
        private T _valor;
        public T Valor
        {
            get
            {
                return _valor;
            }
            set
            {
                _valor = value;
                OnPropertyChanged("Valor");
            }
        }

        /// <summary>
        /// Obtém e define IsValid.
        /// </summary>
        /// <remarks>Indica se este objecto é válido ou inválido. Ou seja se cumpre todas as regras de validação.</remarks>
        private bool _isValido;
        public bool IsValido
        {
            get
            {
                return _isValido;
            }
            set
            {
                _isValido = value;
                OnPropertyChanged("IsValido");
            }
        }



        public ValidatableObject()
        {
            _isValido = true;
            _erros = new List<string>();
            _regrasValidacao = new List<IValidationRule<T>>();
        }



        public bool Validate()
        {
            //Caso este objecto já tenha sido validado antes e não tenha cumprido as regras é necessário limpar a lista de erros, para limpar as
            //mensagens de erros anteriores.
            Erros.Clear();

            //Percorre-se a lista das regras de validação para este objecto (Validations).
            //Cada ValidationRule contém uma mensagem de erro e um método para verificar se este object cumpre
            //a regra.
            //p => !p.Check(Value) onde p é uma ValidationRule da lista de Validations. Verifica-se
            //se este objecto cumpre a regra (p) utilizando o método Check.
            //p => p.ValidationMensagem onde p é é uma ValidationRule da lista de Validations. Obtem a 
            //mensagem de erro da ValidationRule (p).
            //Os últimos dois comentários juntos obtêm o conjunto das mensagens de erros das regras de
            //validação que não foram cumpridas por este objecto.
            IEnumerable<string> errors = RegrasValidacao.Where(p => !p.Check(Valor)).Select(p => p.ValidationMensagem);

            //Guardar a lista de mensagens de erro na propriedade Errors.
            Erros = errors.ToList();

            //A propriedade IsValid é true se a lista de erros estiver vazia (cumpriu-se todas as regras de validação) ou false se houver pelo menos uma 
            //mensagem de erro na lista, ou seja, nem todas as
            //regras foram cumpridas.
            IsValido = !Erros.Any();

            return IsValido;
        }

    }
}

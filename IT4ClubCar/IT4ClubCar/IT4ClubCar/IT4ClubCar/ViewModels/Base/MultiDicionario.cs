using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Base
{
    /// <summary>
    /// Classe que permite instanciar um multidicionário. Num multidicionário cada chave pode conter mais
    /// do que um valor associado.
    /// </summary>
    /// <typeparam name="TKey">Tipo de objecto que vai representar a chave do dicionário.</typeparam>
    /// <typeparam name="TValue">Tipo de objecto que vai representar os valores a que uma chave pode estar associada.</typeparam>
    class MultiDicionario<TKey,TValue> : Dictionary<TKey,List<TValue>>
    {
        /// <summary>
        /// Verifica se o multidicionário já contém uma determinada chave, criando-a e associando-a a uma lista vazia
        /// caso ainda não exista.
        /// </summary>
        /// <param name="key"></param>
        private void VerificarChave(TKey key)
        {
            //Verificar se multidicionário contém chave.
            if(ContainsKey(key))
            {
                //O multidicionário contém esta chave.

                //Se a chave ainda não tiver sido usada, o tvalue ainda não foi inicializado.
                //Verificar se já foi usado, se não tiver sido, inicializar o tvalue (que é uma lista).
                if (this[key] == null)
                    this[key] = new List<TValue>();
            }
            else
                //Chave ainda não existe. Criá-la e associá-la a uma lista vazia.
                this[key] = new List<TValue>(1);
        }



        /// <summary>
        /// Associa um elemento a uma determinada chave.
        /// </summary>
        /// <param name="key">Chave à qual o elemento deve ser associado.</param>
        /// <param name="valor">Elemento a ser associado.</param>
        public void AdicionarValor(TKey key, TValue valor)
        {
            //Verifica-se se a chave existe no multidicionário. Este método executa acções tendo em conta se existe ou não.
            VerificarChave(key);

            //Adicionar o elemento (valor) à lista de valores associados à chave.
            this[key].Add(valor);
        }



        /// <summary>
        /// Associa um conjunto de elementos a uma determinada chave.
        /// </summary>
        /// <param name="key">Chave à qual os elementos devem ser associados</param>
        /// <param name="valores">Elementos a serem associados.</param>
        public void AdicionarValores(TKey key, IEnumerable<TValue> valores)
        {
            //Verifica-se se a chave existe no multidicionário. Este método executa acções tendo em conta se existe ou não.
            VerificarChave(key);

            //Adicionar os elementos (valores) à lista de valores associados à chave.
            this[key].AddRange(valores);
        }



        /// <summary>
        /// Remove um elemento do conjunto de valores associados a uma chave.
        /// </summary>
        /// <param name="key">Chave da qual o elemento deve ser removido.</param>
        /// <param name="valor">Elemento a ser removido.</param>
        public void RemoverValor(TKey key, TValue valor)
        {
            //Verificar se chave passada como parâmetro existe
            if(ContainsKey(key))
                //Existe. Remover elemento da chave.
                this[key].Remove(valor);
        }



        /// <summary>
        /// Remove um conjunto de elementos do conjunto de valores associados a uma chave.
        /// </summary>
        /// <param name="key">Chave da qual os elementos devem ser removidos.</param>
        /// <param name="valor">Elementos a serem removidos.</param>
        public void RemoverValores(TKey key, TValue valores)
        {
            //Verificar se chave passada como parâmetro existe
            if (ContainsKey(key))
                //Existe. Remover elementos da chave.
                this[key].Remove(valores);
        }



        /// <summary>
        /// Remove um conjunto de elementos do conjunto de valores associados a uma chave.
        /// </summary>
        /// <param name="key">Chave da qual os elementos devem ser removidos.</param>
        /// <param name="valor">Elementos a serem removidos.</param>
        public void RemoverValores(TKey key, Predicate<TValue> valores)
        {
            //O parâmetro de valores tem de ser do tipo Predicate porque o método RemoveAll aceita um
            //parâmetro desse tipo.

            //Verificar se chave passada como parâmetro existe
            if (ContainsKey(key))
                //Existe. Remover elementos.
                this[key].RemoveAll(valores);
        }



    }
}

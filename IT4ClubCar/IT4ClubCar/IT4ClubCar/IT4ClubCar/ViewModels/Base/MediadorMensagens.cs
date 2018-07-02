using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Base
{
    sealed class MediadorMensagensService
    {
        //Adicionar novas mensagens neste enum.
        public enum ViewModelMensagens { JogadorAEditar, JogadorAdicionado, JogadorRemovido, NovoJogo, ProTipConteudo, JogadorPontuacaoAEditar, BuracoPontuacaoAEditar, GuardarPontuacoes, PontuacaoAMostrar, NomeAMostrar, AFecharPopup, CampoAtual, BuracoAtual, JogadorATerminarJogo, AFecharPopupTerminarJogo, JogoAtual, JogadorAEnviarScorecard, PedirPorJogo };

        private static MediadorMensagensService _instancia = new MediadorMensagensService();

        public static MediadorMensagensService Instancia
        {
            get
            {
                return _instancia;
            }
        }

        private MultiDicionario<ViewModelMensagens, Action<object>> _listaRelacoes;
        public MultiDicionario<ViewModelMensagens,Action<object>> ListaRelacoes
        {
            get
            {
                if(_listaRelacoes == null) _listaRelacoes = new MultiDicionario<ViewModelMensagens, Action<object>>();
                return _listaRelacoes;
            }
        }



        public void Registar(ViewModelMensagens mensagemAQualRegistar, Action<object> metodoAExecutar)
        {
            ListaRelacoes.AdicionarValor(mensagemAQualRegistar, metodoAExecutar);
        }



        public void Avisar(ViewModelMensagens mensagem, object args)
        {
            //Não utilizei um for porque estava a confudir o i como a key a verificar.
            //ver melhor depois.
            if (ListaRelacoes.ContainsKey(mensagem))
            {
                foreach (Action<object> metodo in ListaRelacoes[mensagem])
                {
                    metodo.Invoke(args);
                }
            }
        }



        public void ResetMensagens(ViewModelMensagens mensagem)
        {
            if(ListaRelacoes.ContainsKey(mensagem))
                ListaRelacoes[mensagem].Clear();
        }

    }
}

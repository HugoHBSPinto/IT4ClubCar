using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class JogadorWrapperViewModel
    {
        private JogadorModel _jogadorModel;

        /// <summary>
        /// Obtém e define o Nome.
        /// </summary>
        public string Nome
        {
            get
            {
                return _jogadorModel.Nome;
            }
            set
            {
                _jogadorModel.Nome = value;
            }
        }

        /// <summary>
        /// Obtém e define o Genero.
        /// </summary>
        public GeneroWrapperViewModel Genero { get; set; }

        /// <summary>
        /// Obtém e define a string base 64 da foto.
        /// </summary>
        public string FotoBase64 { get; set; }

        /// <summary>
        /// Obtém a foto.
        /// </summary>
        public ImageSource Foto { get; }

        /// <summary>
        /// Obtém e define o Handicap.
        /// </summary>
        public HandicapWrapperViewModel Handicap { get; set; }

        /// <summary>
        /// Obtém e define o Tee.
        /// </summary>
        public TeeWrapperViewModel Tee { get; set; }

        public ObservableCollection<PontuacaoWrapperViewModel> Pontuacoes { get; set; }



        public JogadorWrapperViewModel(JogadorModel jogadorModel)
        {
            _jogadorModel = jogadorModel;
            Genero = new GeneroWrapperViewModel(_jogadorModel.Genero);
            Tee = new TeeWrapperViewModel(_jogadorModel.Tee);
            Handicap = new HandicapWrapperViewModel(_jogadorModel.Handicap);
            Pontuacoes = new ObservableCollection<PontuacaoWrapperViewModel>(_jogadorModel.Pontuacoes.Select(p => new PontuacaoWrapperViewModel(p)));
        }



        public void AdicionarPontuacao(PontuacaoWrapperViewModel pontuacao)
        {
            Pontuacoes.Add(pontuacao);
            _jogadorModel.Pontuacoes.Add(pontuacao.ObterModelo());
        }
        
        
        
        public JogadorModel ObterModel()
        {
            return _jogadorModel;
        }



    }
}

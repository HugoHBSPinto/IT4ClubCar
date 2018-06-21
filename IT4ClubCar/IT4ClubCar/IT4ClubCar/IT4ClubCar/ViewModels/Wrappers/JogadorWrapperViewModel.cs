using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.Services.Camera;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using IT4ClubCar.IT4ClubCar.ViewModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class JogadorWrapperViewModel : ExtendedBindableObject
    {
        private JogadorModel _jogadorModel;

        /// <summary>
        /// Obtém e define o Nome.
        /// </summary>
        public string Nome
        {
            get
            {
                if (Bloqueado)
                    return "Indefinido";

                return _jogadorModel.Nome;
            }
            set
            {
                _jogadorModel.Nome = value;
                OnPropertyChanged("Nome");
            }
        }

        /// <summary>
        /// Obtém e define o Email.
        /// </summary>
        public string Email
        {
            get
            {
                return _jogadorModel.Email;
            }
            set
            {
                _jogadorModel.Email = value;
            }
        }

        /// <summary>
        /// Obtém e define o Genero.
        /// </summary>
        public GeneroWrapperViewModel Genero { get; set; }

        /// <summary>
        /// Obtém e define a string base 64 da foto.
        /// </summary>
        public string FotoBase64
        {
            get
            {
                return _jogadorModel.Foto64Base;
            }
            set
            {
                _jogadorModel.Foto64Base = value;
                
                Foto = BytesHandlerHelper.ConverterBase64EmImageSource(_jogadorModel.Foto64Base);
            }
        }

        /// <summary>
        /// Obtém a foto.
        /// </summary>
        private ImageSource _foto;
        public ImageSource Foto
        {
            get
            {
                if (Bloqueado)
                    return ImageSource.FromFile("Player.png");

                return _foto;
            }
            set
            {
                _foto = value;
                OnPropertyChanged("Foto");
            }
        }

        /// <summary>
        /// Obtém e define o Handicap.
        /// </summary>
        public HandicapWrapperViewModel Handicap { get; set; }

        /// <summary>
        /// Obtém e define o Tee.
        /// </summary>
        public TeeWrapperViewModel Tee { get; set; }

        /// <summary>
        /// Obtém as Pontuacoes.
        /// </summary>
        public ObservableCollection<PontuacaoWrapperViewModel> Pontuacoes { get; set; }

        /// <summary>
        /// Obtém o Bloqueado.
        /// </summary>
        public bool Bloqueado
        {
            get
            {
                return _jogadorModel == null;
            }
        }



        public JogadorWrapperViewModel()
        {
            
        }



        public JogadorWrapperViewModel(JogadorModel jogadorModel)
        {
            _jogadorModel = jogadorModel;
            Genero = new GeneroWrapperViewModel(_jogadorModel.Genero);
            Tee = new TeeWrapperViewModel(_jogadorModel.Tee);
            Handicap = new HandicapWrapperViewModel(_jogadorModel.Handicap);
            Pontuacoes = new ObservableCollection<PontuacaoWrapperViewModel>(_jogadorModel.Pontuacoes.Select(p => new PontuacaoWrapperViewModel(p)));
        }



        public void DefinirModel(JogadorModel jogadorModel)
        {
            _jogadorModel = jogadorModel;
            Nome = jogadorModel.Nome;
            Email = jogadorModel.Email;
            FotoBase64 = jogadorModel.Foto64Base;
            Genero = new GeneroWrapperViewModel(_jogadorModel.Genero);
            Tee = new TeeWrapperViewModel(_jogadorModel.Tee);
            Handicap = new HandicapWrapperViewModel(_jogadorModel.Handicap);
            Pontuacoes = new ObservableCollection<PontuacaoWrapperViewModel>(_jogadorModel.Pontuacoes.Select(p => new PontuacaoWrapperViewModel(p)));
        }



        public void ResetJogador()
        {
            if (!Bloqueado)
            {
                Nome = String.Empty;
                Email = String.Empty;
                FotoBase64 = String.Empty;
                Genero = null;
                Tee = null;
                Handicap = null;
                Pontuacoes = null;

                _jogadorModel = null;
            }
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

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
        /// Obtém o Id.
        /// </summary>
        public int Id
        {
            get
            {
                return _jogadorModel.Id;
            }
        }

        /// <summary>
        /// Obtém e define o Nome.
        /// </summary>
        public string Nome
        {
            get
            {
                if (IsBloqueado)
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
                OnPropertyChanged("Email");
            }
        }

        /// <summary>
        /// Obtém a Senha.
        /// </summary>
        public string Senha
        {
            get
            {
                return _jogadorModel.Senha;
            }
        }

        /// <summary>
        /// Obtém e define o Genero.
        /// </summary>
        public GeneroWrapperViewModel Genero { get; set; }

        /// <summary>
        /// Obtém e define a FotoBase64.
        /// </summary>
        public string FotoBase64
        {
            get
            {
                return _jogadorModel.FotoBase64;
            }
            set
            {
                _jogadorModel.FotoBase64 = value;
                OnPropertyChanged("Foto");
            }
        }

        /// <summary>
        /// Obtém a foto.
        /// </summary>
        public ImageSource Foto
        {
            get
            {
                if (IsBloqueado)
                    return ImageSource.FromFile("Player.png");

                return BytesHandlerHelper.ConverterBase64EmImageSource(FotoBase64);
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
        public bool IsBloqueado
        {
            get
            {
                return _jogadorModel == null;
            }
        }

        /// <summary>
        /// Obtém e define EmUso.
        /// </summary>
        public bool EmUso { get; set; }

        /// <summary>
        /// Obtém e define PontuacaoEmUso.
        /// </summary>
        public bool PontuacaoEmUso { get; set; }

        /// <summary>
        /// Obtém e define NomeEmUso.
        /// </summary>
        public bool NomeEmUso { get; set; }

        /// <summary>
        /// Obtém o JogadorInscrito.
        /// </summary>
        /// <remarks>Se devolver true quer dizer que é um jogador inscrito. Se for false quer dizer 
        /// que é Guest.</remarks>
        public bool IsJogadorInscrito
        {
            get
            {
                return !Id.Equals(-1);
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
            FotoBase64 = jogadorModel.FotoBase64;
            Genero = new GeneroWrapperViewModel(_jogadorModel.Genero);
            Tee = new TeeWrapperViewModel(_jogadorModel.Tee);
            Handicap = new HandicapWrapperViewModel(_jogadorModel.Handicap);
            Pontuacoes = new ObservableCollection<PontuacaoWrapperViewModel>(_jogadorModel.Pontuacoes.Select(p => new PontuacaoWrapperViewModel(p)));
        }



        public void ResetJogador()
        {
            if (!IsBloqueado)
            {
                Nome = String.Empty;
                Email = String.Empty;
                FotoBase64 = null;
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

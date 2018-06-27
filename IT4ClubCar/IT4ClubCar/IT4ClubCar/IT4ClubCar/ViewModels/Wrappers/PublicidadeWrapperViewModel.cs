using IT4ClubCar.IT4ClubCar.Models;
using IT4ClubCar.IT4ClubCar.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Wrappers
{
    class PublicidadeWrapperViewModel : BaseWrapperViewModel
    {
        private PublicidadeModel _publicidadeModel;

        /// <summary>
        /// Obtém o Titulo.
        /// </summary>
        public string Titulo
        {
            get
            {
                return _publicidadeModel.Titulo;
            }
        }

        /// <summary>
        /// Obtém a Descricao.
        /// </summary>
        public string Descricao
        {
            get
            {
                return _publicidadeModel.Descricao;
            }
        }

        /// <summary>
        /// Obtém a Imagem.
        /// </summary>
        public ImageSource Imagem
        {
            get
            {
                return _publicidadeModel.Imagem;
            }
        }


        public PublicidadeWrapperViewModel(PublicidadeModel publicidadeModel)
        {
            _publicidadeModel = publicidadeModel;
        }

    }
}

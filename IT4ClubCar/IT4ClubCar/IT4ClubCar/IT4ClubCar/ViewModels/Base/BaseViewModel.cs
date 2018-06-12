﻿using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Base
{
    /// <summary>
    /// Classe base para todos os viewmodels, herda da classe BindableObject e contém
    /// instâncias do INavigationService, para permitir navegar entre as páginas e do IDialogService para
    /// permitir mostrar popups.
    /// </summary>
    class BaseViewModel : ExtendedBindableObject
    {
        //O termo [Dependency] faz parte do Unity (biblioteca utilizada para IOC). Indica que é uma dependency property. Ou seja
        //vai obter o seu valor através de injection.

        [Dependency]
        public INavigationService NavigationService { get; set; }

        [Dependency]
        public IDialogService DialogService { get; set; }


        /// <summary>
        /// Construtor com parâmetros para o viewmodel.
        /// </summary>
        /// <param name="navigationService">Instância do INavegationService para permitir ao viewmodel mudar de página.</param>
        /// <param name="dialogService">Instância do IDialogService para permitir ao viewmodel msotrar pop ups/mensagens.</param>
        public BaseViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
        }

    }
}


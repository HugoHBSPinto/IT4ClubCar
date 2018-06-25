using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Xamarin.Forms;
using System.Reflection;
using System.Diagnostics;
using IT4ClubCar.IT4ClubCar.Services.Buracos;
using IT4ClubCar.IT4ClubCar.Services.Campo;
using IT4ClubCar.IT4ClubCar.Services.Genero;
using IT4ClubCar.IT4ClubCar.Services.Metrico;
using IT4ClubCar.IT4ClubCar.Services.ModoJogo;
using IT4ClubCar.IT4ClubCar.Services.Tee;
using IT4ClubCar.IT4ClubCar.Services.TeeDistancias;
using IT4ClubCar.IT4ClubCar.ViewModels.UserControls;
using IT4ClubCar.IT4ClubCar.ViewModels.Popups;
using IT4ClubCar.IT4ClubCar.Services.Camera;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Base
{
    /// <summary>
    /// Classe que encontra e atribui o viewmodel correspondente a uma determinada view.
    /// </summary>
    class ViewModelLocator
    {
        /// <summary>
        /// Propriedade bool iniciada a false que quando atualizada executa o evento 
        /// OnDefinirViewModelAutomaticamenteChanged, que por sua vez encontra e define o 
        /// viewmodel a uma view.
        /// </summary>
        public static readonly BindableProperty DefinirViewModelAutomaticamenteProperty =
            BindableProperty.CreateAttached("DefinirViewModelAutomaticamente", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnDefinirViewModelAutomaticamenteChanged);



        /// <summary>
        /// Devolve o valor da propriedade DefinirViewModelAutomaticamente da classe passada como parâmetro.
        /// </summary>
        /// <param name="bindable">View a obter-se o valor da propriedade DefinirViewModelAutomaticamente.</param>
        /// <returns>Valor da propriedade DefinirViewModelAutomaticamente.</returns>
        public static bool GetDefinirViewModelAutomaticamente(BindableObject bindable)
        {
            return (bool)bindable.GetValue(DefinirViewModelAutomaticamenteProperty);
        }



        /// <summary>
        /// Atualiza o valor da propriedade DefinirViewModelAutomaticamente da view passada como parâmetro.
        /// </summary>
        /// <param name="bindable">View na qual se quer atualizar o valor da propriedade</param>
        /// <param name="value">Novo valor da propriedade DefinirViewModelAutomaticamente.</param>
        public static void SetDefinirViewModelAutomaticamente(BindableObject bindable, bool value)
        {
            bindable.SetValue(DefinirViewModelAutomaticamenteProperty, value);
        }



        static ViewModelLocator()
        {
            //Registar viewmodels existentes. Se criar-se um novo adicionar o seu registo aqui.
            App.Container.RegisterType<MenuPrincipalViewModel>();
            App.Container.RegisterType<JogadorUserControlViewModel>();
            App.Container.RegisterType<EditarJogadorPopupViewModel>();
            App.Container.RegisterType<JogoConfiguracaoViewModel>();
            App.Container.RegisterType<JogoViewModel>();
            App.Container.RegisterType<ProTipPopupViewModel>();
            App.Container.RegisterType<PontuacoesPopupViewModel>();
            App.Container.RegisterType<DefinicaoPontuacaoUserControlViewModel>();
            App.Container.RegisterType<ScorecardPopupViewModel>();
            App.Container.RegisterType<JogadorPontuacaoUserControlViewModel>();
            App.Container.RegisterType<JogadorNomeUserControlViewModel>();



            //Registar services existentes. Se criar-se um novo adicionar o seu registo aqui.
            App.Container.RegisterType<INavigationService, IT4ClubCarNavigationService>();
            App.Container.RegisterType<IDialogService, IT4ClubCarDialogService>();
            App.Container.RegisterType<IBuracosService, BuracosService>();
            App.Container.RegisterType<ICampoService, CampoService>();
            App.Container.RegisterType<IGeneroService, GeneroService>();
            App.Container.RegisterType<IHandicapService, HandicapService>();
            App.Container.RegisterType<IMetricoService, MetricoService>();
            App.Container.RegisterType<IModoJogoService, ModoJogoService>();
            App.Container.RegisterType<ITeeService, TeeService>();
            App.Container.RegisterType<ITeeDistanciaService, TeeDistanciaService>();
            App.Container.RegisterType<ICameraService, CameraService>();
        }



        /// <summary>
        /// Encontra e define o viewmodel de uma determinada view.
        /// </summary>
        /// <param name="view">View que está a pedir o viewmodel.</param>
        /// <param name="oldValue">Antigo valor da propriedade DefinirViewModelAutomaticamente. Não usado.</param>
        /// <param name="newValue">Novo valor da propriedade DefinirViewModelAutomaticamente. Não usado.</param>
        private static void OnDefinirViewModelAutomaticamenteChanged(BindableObject view, object oldValue, object newValue)
        {
            //Este método encontra o viewmodel correspondente a uma view utilizando os nomes.
            //Recorre-se a uma nomenclatura comum. Tanto a view como o viewmodel têm o mesmo nome,
            //apenas o sufixo muda. Ex: a janela EditarJogador, a view chama-se EditarJogadorView e
            //o viewmodel chama-se EditarJogadorViewModel. O nome é igual apenas o sufixo muda.
            //Além disso, também se considera que todas as views estão dentro de uma pasta chamada
            //Views e todos os viewmodels estão dentro de uma pasta chamada ViewModels, sendo que
            //ambas essas pastas encontram-se no mesmo nível da estrutura.


            //Primeiro obtém-se o nome completo da view. O nome completo inclui tanto o namespace como o nome
            //da view. Ex: se fosse a view EditarJogadorView a pedir o viewmodel, a variável fullName 
            //teria o valor : TestinEditable.Views.EditPersonView.
            string fullName = view.GetType().FullName;

            //O segundo passo será obter o nome completo do viewmodel através do nome completo da view. Mais uma
            //vez, ao obtermos o nome completo queremos tanto o namespace como o nome do viewmodel.
            //Como a pasta das Views e dos ViewModels encontram-se no mesmo nível da estrutura de pastas,
            //basta mudar a parte '.Views.' para '.ViewModels.' para termos o namespace correto.
            fullName = fullName.Replace(".Views.", ".ViewModels.");

            //Agora em vez de termos TestinEditable.Views.EditPersonView temos 
            //TestinEditable.ViewModels.EditPersonView, ou seja estamos na pasta onde os viewmodels encontram-se.
            //Só falta alterar a parte final, do nome da view para o nome do viewmodel. Como tanto a view como o
            //viewmodel têm o mesmo nome, apenas alterando-se o sufixo, para obtermos o nome do viewmodel basta
            //adicionar 'Model' no fim do nome.
            fullName += "Model";

            //Agora temos o nome completo do viewmodel correspondente à view. Passou-se de 
            //TestinEditable.Views.EditPersonView para TestinEditable.ViewModels.EditPersonViewModel.
            //Agora basta criar o viewmodel. Primeiro obtém-se o Tipo do viewmodel e depos cria-se
            //uma instância do mesmo. Para se obter o Tipo utiliza-se um método existente que aceita o 
            //nome completo.
            Type viewModelType = Type.GetType(fullName);

            //O ViewModel pode não existir (ex: não foi criado). Verifica-se se o tipo obtido é null, se for sai-se do
            //método não definindo-se o viewmodel.
            if (viewModelType == null)
                return;

            //Agora que temos o tipo, obtém-se uma instância do mesmo resolvendo o tipo no Container da App onde todos os viewmodels
            //existentes estão registados.
            var viewModel = App.Container.Resolve(viewModelType);

            //Agora já temos o viewmodel correspondente. Basta atribuir o viewmodel ao BindingContext
            //da view (ou bindable).
            ((Element)view).BindingContext = viewModel;
        }

    }
}

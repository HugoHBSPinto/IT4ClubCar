using IT4ClubCar.IT4ClubCar.Services.Navegacao;
using IT4ClubCar.IT4ClubCar.Services.Dialog;
using IT4ClubCar.IT4ClubCar.Views;
using System;
using Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace IT4ClubCar
{
	public partial class App : Application
	{
        /// <summary>
        /// Obtém e define o Container.
        /// </summary>
        /// <remarks>Propriedade estática utilizada pelas classes que utilizam depedency injection.</remarks>
        public static UnityContainer Container { get; set; }



        public App ()
		{
			InitializeComponent();

            //Inicializar a propriedade Container.
            Container = new UnityContainer();

            //Registar os tipos para a dependency injection.

            //Quando se pedir um INavigationService passa-se uma instância do IT4ClubCarNavigationService (passa-se sempre a 
            //mesma instância).
            Container.RegisterInstance<INavigationService>(new IT4ClubCarNavigationService());

            //Quando se pedir um IDialogService passa-se uma instância do IT4ClubCarDialogService (passa-se sempre a mesma instância).
            Container.RegisterInstance<IDialogService>(new IT4ClubCarDialogService());

            //Criar uma nova instância da view MainPage, sendo utilizada para definir a primeira página da Navegação e a primeira
            //página a aparecer.
            MainPageView mainPage = new MainPageView();

            //Definir como a página principal da navegação.
            Current.MainPage = mainPage;

            //Definir como a primeira página a aparecer.
            MainPage = mainPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

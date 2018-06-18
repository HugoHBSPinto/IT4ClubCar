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
        /// Obt�m e define o Container.
        /// </summary>
        /// <remarks>Propriedade est�tica utilizada pelas classes que utilizam depedency injection.</remarks>
        public static UnityContainer Container { get; set; }



        public App ()
		{
			InitializeComponent();

            //Inicializar a propriedade Container.
            Container = new UnityContainer();

            //Criar uma nova inst�ncia da view MainPage, sendo utilizada para definir a primeira p�gina da Navega��o e a primeira
            //p�gina a aparecer.
            MainPageView mainPage = new MainPageView();

            //Definir como a p�gina principal da navega��o.
            Current.MainPage = mainPage;

            //Definir como a primeira p�gina a aparecer.
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

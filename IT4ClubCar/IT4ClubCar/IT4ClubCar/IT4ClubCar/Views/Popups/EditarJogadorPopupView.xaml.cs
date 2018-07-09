using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IT4ClubCar.IT4ClubCar.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarJogadorPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
		public EditarJogadorPopupView ()
		{
			InitializeComponent ();
		}



        protected override void OnDisappearing()
        {
            BindingContext = null;
            base.OnDisappearing();
            GC.Collect();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IT4ClubCar.IT4ClubCar.Views
{
	public partial class MenuPrincipalView : ContentPage
	{
		public MenuPrincipalView()
		{
            InitializeComponent();
		}

        protected override void OnDisappearing()
        {
            BindingContext = null;
            base.OnDisappearing();
            GC.Collect();
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IT4ClubCar.IT4ClubCar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JogoConfiguracaoView : ContentPage
	{
		public JogoConfiguracaoView ()
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
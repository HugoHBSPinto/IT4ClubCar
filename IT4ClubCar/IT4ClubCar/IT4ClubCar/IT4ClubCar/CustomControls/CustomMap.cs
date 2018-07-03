using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace IT4ClubCar.IT4ClubCar.CustomControls
{
    class CustomMap : Map
    {
        public static BindableProperty PosicaoProperty =
        BindableProperty.Create("Posicao", typeof(MapSpan), typeof(CustomMap), MapSpan.FromCenterAndRadius(new Position(37.147164, -8.354), Distance.FromMeters(80)), BindingMode.TwoWay);

        public static BindableProperty BuracoPinPosicaoProperty =
        BindableProperty.Create("BuracoPinPosicao", typeof(Position), typeof(CustomMap), new Position(1.0, -1.0), BindingMode.TwoWay);

        public static BindableProperty TeePinPosicaoProperty =
        BindableProperty.Create("TeePinPosicao", typeof(Position), typeof(CustomMap), new Position(1.0, -1.0), BindingMode.TwoWay);

        public static BindableProperty MeioPinPosicaoProperty =
        BindableProperty.Create("MeioPinPosicao", typeof(Position), typeof(CustomMap), new Position(1.0, -1.0), BindingMode.TwoWay);

        public MapSpan Posicao
        {
            get
            {
                return (MapSpan)GetValue(PosicaoProperty);
            }
            set
            {
                SetValue(PosicaoProperty, value);
            }
        }

        public Position BuracoPinPosicao
        {
            get
            {
                return (Position)GetValue(BuracoPinPosicaoProperty);
            }
            set
            {
                SetValue(BuracoPinPosicaoProperty, value);
            }
        }

        public Position TeePinPosicao
        {
            get
            {
                return (Position)GetValue(TeePinPosicaoProperty);
            }
            set
            {
                SetValue(TeePinPosicaoProperty, value);
            }
        }

        public Position MeioPinPosicao
        {
            get
            {
                return (Position)GetValue(MeioPinPosicaoProperty);
            }
            set
            {
                SetValue(MeioPinPosicaoProperty, value);
            }
        }

        public CustomMap() : base() { }
    }
}

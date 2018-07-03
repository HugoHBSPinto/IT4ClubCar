using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IT4ClubCar.IT4ClubCar.CustomControls;
using IT4ClubCar.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using Android.Gms.Maps.Model;

[assembly: ExportRenderer(typeof(CustomMap),typeof(MapCustomRenderer))]
namespace IT4ClubCar.Droid.CustomRenderers
{
    class MapCustomRenderer : MapRenderer, IOnMapReadyCallback
    {
        private CustomMap _customMap;
        private GoogleMap _mapa;

        private Marker _buracoMarker;
        private Marker _teeMarker;
        private Marker _meioMarker;


        public MapCustomRenderer(Context c) : base(c)
        {
            
        }



        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            
            _mapa = map;
            
            InicializarMapa();
        }



        private void InicializarMapa()
        {
            _customMap.MoveToRegion(_customMap.Posicao);

            //Buraco Marker.
            var buracoPin = new MarkerOptions();
            buracoPin.SetPosition(new LatLng(_customMap.BuracoPinPosicao.Latitude, _customMap.BuracoPinPosicao.Longitude));
            buracoPin.Draggable(true);
            _buracoMarker = _mapa.AddMarker(buracoPin);

            //Tee Marker.
            var teePin = new MarkerOptions();
            teePin.SetPosition(new LatLng(_customMap.TeePinPosicao.Latitude, _customMap.TeePinPosicao.Longitude));
            teePin.Draggable(true);
            _teeMarker = _mapa.AddMarker(teePin);

            //Tee Marker.
            var meioPin = new MarkerOptions();
            meioPin.SetPosition(new LatLng(_customMap.MeioPinPosicao.Latitude, _customMap.MeioPinPosicao.Longitude));
            meioPin.Draggable(true);
            _meioMarker = _mapa.AddMarker(meioPin);

            _mapa.MarkerDrag += Map_MarkerDrag;
        }



        private void Map_MarkerDrag(object sender, GoogleMap.MarkerDragEventArgs e)
        {
            if(e.Marker.Equals(_buracoMarker))
                _customMap.BuracoPinPosicao = new Position(e.Marker.Position.Latitude, e.Marker.Position.Longitude);
            else
                if (e.Marker.Equals(_teeMarker))
                    _customMap.TeePinPosicao = new Position(e.Marker.Position.Latitude, e.Marker.Position.Longitude);
            else
                if (e.Marker.Equals(_meioMarker))
                    _customMap.MeioPinPosicao = new Position(e.Marker.Position.Latitude, e.Marker.Position.Longitude);
        }



        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            Control.GetMapAsync(this);

            if (e.OldElement != null || e.NewElement != null)
                _customMap = e.NewElement as CustomMap;
        }

    }
}
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
using IT4ClubCar.IT4ClubCar.ViewModels.Base;

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

        private Polyline _linhaBuracoMeio;
        private Polyline _linhaTeeMeio;


        public MapCustomRenderer(Context c) : base(c)
        {
            //Ouvir pela mudança do tee selecionado.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.NovaPosicaoTeePin, p => AtualizarPosicaoPinTee(novaPosicao: (Position)p));
            //Ouvir pelo pedido de fazer reset à posição do meio.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.NovaPosicaoMeio, p => AtualizarPosicaoPinMeio(novaPosicao: (Position)p));
            //Ouvir pelo pedido de fazer reste à posição do buraco.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.NovaPosicaoBuraco, p => AtualizarPosicaoPinBuraco((Position)p));
            //Ouvir pelo pedido de bloquear ou desbloquear a possibilidade de mover o pin do tee.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.AlterarDraggablePinTee, p => AlterarEstadoPinTee());
            //Ouvir pelo pedido de fazer reset à region do mapa.
            MediadorMensagensService.Instancia.Registar(MediadorMensagensService.ViewModelMensagens.ResetRegionMapa, p => AtualizarRegionMapa((MapSpan)p));
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
            buracoPin.SetTitle("Buraco");
            _buracoMarker = _mapa.AddMarker(buracoPin);

            //Tee Marker.
            var teePin = new MarkerOptions();
            teePin.SetPosition(new LatLng(_customMap.TeePinPosicao.Latitude, _customMap.TeePinPosicao.Longitude));
            teePin.Draggable(true);
            teePin.SetTitle("Tee");
            _teeMarker = _mapa.AddMarker(teePin);
            _teeMarker.Draggable = false;

            //Meio Marker.
            var meioPin = new MarkerOptions();
            meioPin.SetPosition(new LatLng(_customMap.MeioPinPosicao.Latitude, _customMap.MeioPinPosicao.Longitude));
            meioPin.Draggable(true);
            meioPin.SetTitle("Meio");
            _meioMarker = _mapa.AddMarker(meioPin);
            
            _mapa.MarkerDrag += _mapa_MarkerDrag;
        }



        private void _mapa_MarkerDrag(object sender, GoogleMap.MarkerDragEventArgs e)
        {
            _linhaBuracoMeio?.Remove();
            _linhaBuracoMeio = _mapa.AddPolyline(new PolylineOptions().Add(_buracoMarker.Position, _meioMarker.Position).InvokeWidth(4));

            _linhaTeeMeio?.Remove();
            _linhaTeeMeio = _mapa.AddPolyline(new PolylineOptions().Add(_meioMarker.Position, _teeMarker.Position).InvokeWidth(4));

            if (e.Marker.Equals(_teeMarker))
                _customMap.TeePinPosicao = new Position(e.Marker.Position.Latitude, e.Marker.Position.Longitude);
            else
                if (e.Marker.Equals(_meioMarker))
                _customMap.MeioPinPosicao = new Position(e.Marker.Position.Latitude, e.Marker.Position.Longitude);
        }



        /// <summary>
        /// Atualiza a posição do Pin do Tee.
        /// </summary>
        /// <param name="novaPosicao">Nova posição do Pin do Tee.</param>
        private void AtualizarPosicaoPinTee(Position novaPosicao)
        {
            if (_teeMarker == null)
                return;

            _teeMarker.Position = new LatLng(novaPosicao.Latitude, novaPosicao.Longitude);

            _linhaTeeMeio?.Remove();
            _linhaTeeMeio = _mapa.AddPolyline(new PolylineOptions().Add(_meioMarker.Position, _teeMarker.Position).InvokeWidth(4));
        }



        /// <summary>
        /// Atualiza a posição do Pin do Meio.
        /// </summary>
        /// <param name="novaPosicao">Nova posição do Pin Meio.</param>
        private void AtualizarPosicaoPinMeio(Position novaPosicao)
        {
            if (_meioMarker == null)
                return;

            _meioMarker.Position = new LatLng(novaPosicao.Latitude, novaPosicao.Longitude);

            _linhaBuracoMeio?.Remove();
            _linhaBuracoMeio = _mapa.AddPolyline(new PolylineOptions().Add(_buracoMarker.Position, _meioMarker.Position).InvokeWidth(4));

            _linhaTeeMeio?.Remove();
            _linhaTeeMeio = _mapa.AddPolyline(new PolylineOptions().Add(_meioMarker.Position, _teeMarker.Position).InvokeWidth(4));
        }



        /// <summary>
        /// Atualiza a posição do Pin do Buraco.
        /// </summary>
        /// <param name="novaPosicao">Nova posição do Pin do Tee.</param>
        private void AtualizarPosicaoPinBuraco(Position novaPosicao)
        {
            if (_buracoMarker == null)
                return;

            _buracoMarker.Position = new LatLng(novaPosicao.Latitude, novaPosicao.Longitude);
        }



        /// <summary>
        /// Bloqueia ou desbloqueia a posição do pin do tee.
        /// </summary>
        /// <remarks>Tem em conta o valor atual da propriedade IsDraggable do _teeMarker.</remarks>
        private void AlterarEstadoPinTee()
        {
            if (_teeMarker == null)
                return;

            _teeMarker.Draggable = !_teeMarker.Draggable;
        }



        /// <summary>
        /// Atualiza a área mostrada pelo mapa.
        /// </summary>
        /// <param name="novaRegion">Nova region a ser mostrada pelo mapa.</param>
        private void AtualizarRegionMapa(MapSpan novaRegion)
        {
            if (_customMap == null)
                return;

            _customMap.MoveToRegion(novaRegion);
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
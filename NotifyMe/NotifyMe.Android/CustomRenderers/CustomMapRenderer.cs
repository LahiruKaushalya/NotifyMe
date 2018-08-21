using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using CustomRenderer.Droid;
using NotifyMe.Droid;
using NotifyMe.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace CustomRenderer.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        private GoogleMap _map;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.MapLongClick -= OnMapLongClick;
            }

            if (e.NewElement != null)
            {
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            _map = map;
            NativeMap.MapLongClick += OnMapLongClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        private void OnMapLongClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            _map.Clear();
            var position = new Position(e.Point.Latitude, e.Point.Longitude);

            ((CustomMap)Element).OnTap(position);
            ((CustomMap)Element).IsNetworkConnected(MainActivity.IsNetworkConnected());

            var marker = CreateMarker(new Pin() {
                Position = position
            });
            _map.AddMarker(marker);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            return marker;
        }


        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
        
        public Android.Views.View GetInfoContents(Marker marker)
        {
            return null;
        }
    }
}


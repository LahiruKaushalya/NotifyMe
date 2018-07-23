using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace NotifyMe.CustomRenderers
{
    public class CustomMap : Map
    {
        public Position TapPosition { get; set; }
        
        public void OnTap(Position coordinate)
        {
            TapPosition = coordinate;
        }
    }
}

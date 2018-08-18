using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using NotifyMe.CustomRenderers;
using NotifyMe.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace NotifyMe.iOS.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}
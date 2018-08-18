using Android.Content;
using Xamarin.Forms.Platform.Android;
using NotifyMe.CustomRenderers;
using NotifyMe.Droid.CustomRenderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace NotifyMe.Droid.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Gravity = Android.Views.GravityFlags.CenterHorizontal;
                Control.Gravity = Android.Views.GravityFlags.CenterVertical;
            }
        }
    }
}
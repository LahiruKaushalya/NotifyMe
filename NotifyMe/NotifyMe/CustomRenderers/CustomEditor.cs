using Xamarin.Forms;

namespace NotifyMe.CustomRenderers
{
    public class CustomEditor : Editor
    {
        public CustomEditor()
        {
            this.TextChanged += (sender, e) => {
                this.InvalidateMeasure();
            };
        }
    }
}

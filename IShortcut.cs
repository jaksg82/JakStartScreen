using System.Windows.Media.Imaging;

namespace JakStartScreen
{
    internal interface IShortcut
    {
        public string Name { get; set; }
        public string LinkURL { get; set; }
        public BitmapImage Image { get; set; }
    }
}

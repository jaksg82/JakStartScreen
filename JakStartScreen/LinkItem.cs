using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace JakStartScreen
{
    public class LinkItem
    {
        public enum AppType
        {
            Desktop,
            AppX,
            Website
        }

        public string Name { get; set; }
        public string LinkURL { get; set; }
        public string Parent { get; set; }
        public BitmapImage Image { get; set; }
        public AppType Type { get; set; }
        private byte[] _data;

        public byte[] GetImageBytes()
        {
            return _data;
        }

        public LinkItem()
        {
            Uri noBmp = new Uri("/JakStartScreen;component/Assets/NoImage128px.png", UriKind.RelativeOrAbsolute);
            var streamResourceInfo = App.GetResourceStream(noBmp);
            var stream = streamResourceInfo.Stream;
            var byteBuffer = new byte[stream.Length];
            using (stream)
            {
                stream.Read(byteBuffer, 0, byteBuffer.Length);
            }
            Type = AppType.Desktop;
            Image = new BitmapImage(noBmp);
            Name = Language.Strings.NewShortcut;
            LinkURL = "";
            Parent = "";
            _data = byteBuffer;
        }

        public LinkItem(string name, string link, BitmapImage bitmap, AppType type)
        {
            Name = name;
            LinkURL = link;
            Image = bitmap;
            Type = type;
            Parent = "";
            _data = GetImageBytes(bitmap);
        }

        private static byte[] GetImageBytes(BitmapImage bitmap)
        {
            var stream = bitmap.StreamSource;
            var byteBuffer = new byte[stream.Length];
            using (stream)
            {
                stream.Read(byteBuffer, 0, byteBuffer.Length);
            }
            return byteBuffer;
        }
    }
}
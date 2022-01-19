using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace JakStartScreen
{
    public enum IconSize
    {
        Tile1x1,
        Tile1x2,
        Tile1x3,
        Tile2x2,
        Tile2x3,
        Tile2x4
    }

    public class LinkItem : IShortcut
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LinkURL { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public BitmapImage Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IconSize Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Row { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Column { get => throw new NotImplementedException(); set =>  throw new NotImplementedException(); } 


    }
}

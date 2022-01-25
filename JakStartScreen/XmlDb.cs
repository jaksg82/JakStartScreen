using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace JakStartScreen
{
    public static class XmlDb
    {
        public static string LinkViewElement = "LinkView";
        public static string LinkViewTitle = "Title";
        public static string LinkViewUrl = "URL";
        public static string LinkViewImage = "Image";
        public static string LinkViewSize = "Size";
        public static string LinkViewRow = "Row";
        public static string LinkViewCol = "Column";
        public static string LinkViewImageHeight = "Height";
        public static string LinkViewImageWidth = "Width";
        public static string LinkViewImageBytes = "Bytes";

        private static XElement TileSizeToXElement(IconSize size)
        {
            string resSize = "";
            switch (size)
            {
                case IconSize.Tile1x1: resSize = "Tile1x1"; break;
                case IconSize.Tile1x4: resSize = "Tile1x4"; break;
                case IconSize.Tile2x2: resSize = "Tile2x2"; break;
                case IconSize.Tile2x4: resSize = "Tile2x4"; break;
            }
            return new XElement(LinkViewSize, resSize);
        }

        private static XElement ImageToXElement(BitmapImage image, byte[] imageBytes)
        {
            XElement resXml = new XElement(XmlDb.LinkViewImage);
            resXml.Add(new XElement(XmlDb.LinkViewImageHeight, image.PixelHeight));
            resXml.Add(new XElement(XmlDb.LinkViewImageWidth, image.PixelWidth));
            resXml.Add(new XElement(XmlDb.LinkViewImageBytes, Convert.ToBase64String(imageBytes)));
            return resXml;
        }

        private static XElement LinkViewToXElement(LinkItemView itemView)
        {
            XElement lnk = new XElement(XmlDb.LinkViewElement);
            lnk.Add(new XElement(XmlDb.LinkViewTitle, itemView.Title));
            lnk.Add(new XElement(XmlDb.LinkViewUrl, itemView.Link));
            lnk.Add(TileSizeToXElement(itemView.Size));
            lnk.Add(new XElement(XmlDb.LinkViewRow, itemView.Row));
            lnk.Add(new XElement(XmlDb.LinkViewCol, itemView.Column));
            lnk.Add(ImageToXElement(itemView.Image, itemView.ImageBytes));
            return lnk;
        }
    }
}
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

        private static XElement TileSizeToXElement(IconSize size)
        {
            string resSize = "";
            switch (size)
            {
                case IconSize.Tile1x1: resSize = "Tile1x1"; break;
                case IconSize.Tile1x2: resSize = "Tile1x2"; break;
                case IconSize.Tile1x3: resSize = "Tile1x3"; break;
                case IconSize.Tile2x2: resSize = "Tile2x2"; break;
                case IconSize.Tile2x3: resSize = "Tile2x3"; break;
                case IconSize.Tile2x4: resSize = "Tile2x4"; break;
            }
            return new XElement(LinkViewSize, resSize);
        }

        private static XElement ImageToXElement(BitmapImage image)
        {
            byte[] bmpData;
            // Get an Image Stream
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap btmMap = new WriteableBitmap(image);

                // write an image into the stream
                //Extensions.SaveJpeg(btmMap, ms, image.PixelWidth, image.PixelHeight, 0, 100);

                // reset the stream pointer to the beginning
                ms.Seek(0, 0);
                //read the stream into a byte array
                bmpData = new byte[ms.Length];
                ms.Read(bmpData, 0, bmpData.Length);
            }
            //data now holds the bytes of the image
            string resStr = Convert.ToBase64String(bmpData);
            int imgW, imgH;
            imgW = image.PixelWidth;
            imgH = image.PixelHeight;

            XElement resXml = new XElement(XmlDb.LinkViewImage, resStr);
            resXml.SetAttributeValue(XmlDb.LinkViewImageHeight, imgH);
            resXml.SetAttributeValue(XmlDb.LinkViewImageWidth, imgW);
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
            lnk.Add(ImageToXElement(itemView.Image));
            return lnk;
        }
    }
}
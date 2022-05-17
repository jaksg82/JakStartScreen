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
        public static string LinkItemElement = "LinkItem";
        public static string LinkItemType = "LinkType";
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
        public static string AppListRoot = "AppList";

        public static List<LinkItem> LoadAppListDb(string fileName)
        {
            List<LinkItem> list = new List<LinkItem>();
            XDocument xDoc = GetXDocumentFromFileName(fileName);
            XElement xRoot = xDoc.Root;
            if (xRoot.Name == AppListRoot)
            {
                foreach (XElement xElem in xRoot.Elements())
                {
                    list.Add(XElementToLinkItem(xElem));
                }
            }
            return list;
        }

        public static bool SaveAppListDb(string fileName, List<LinkItem> items)
        {
            XDocument resDoc = new XDocument();
            XElement xRoot = new XElement(AppListRoot);
            foreach (LinkItem item in items)
            {
                xRoot.Add(LinkItemToXElement(item));
            }
            resDoc.Add(xRoot);
            resDoc.Declaration = new XDeclaration("1.0", "UTF-8", "yes");
            try
            {
                resDoc.Save(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static XDocument GetXDocumentFromFileName(string FileName)
        {
            XDocument xDoc = new XDocument();
            if (File.Exists(FileName))
            {
                using (StreamReader sr = new StreamReader(new FileStream(FileName, FileMode.Open)))
                {
                    string xStr = sr.ReadToEnd();
                    xDoc = XDocument.Parse(xStr);
                }
            }
            else
            {
                xDoc = new XDocument();
            }
            return xDoc;
        }

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

        private static XElement ImageToXElement(byte[] imageBytes)
        {
            return new XElement(LinkViewImage, Convert.ToBase64String(imageBytes));
        }

        private static XElement LinkViewToXElement(LinkItemView itemView)
        {
            XElement lnk = new XElement(LinkViewElement);
            lnk.Add(new XElement(LinkViewTitle, itemView.Title));
            lnk.Add(new XElement(LinkViewUrl, itemView.Link));
            lnk.Add(TileSizeToXElement(itemView.Size));
            lnk.Add(new XElement(LinkViewRow, itemView.Row));
            lnk.Add(new XElement(LinkViewCol, itemView.Column));
            lnk.Add(ImageToXElement(itemView.ImageBytes));
            return lnk;
        }

        private static XElement LinkItemToXElement(LinkItem item)
        {
            XElement lnk = new XElement(LinkViewElement);
            lnk.Add(new XElement(LinkViewTitle, item.Name));
            lnk.Add(new XElement(LinkViewUrl, item.LinkURL));
            lnk.Add(new XElement(LinkItemType, item.Type));
            lnk.Add(ImageToXElement(item.GetImageBytes()));
            return lnk;
        }

        private static LinkItem XElementToLinkItem(XElement item)
        {
            string itemTitle = "", itemUrl = "", itemBmpBytes = "";
            BitmapImage image = new BitmapImage();
            if (item.Element(LinkViewTitle) != null)
            {
                itemTitle = item.Element(LinkViewTitle).Value;
            }
            if (item.Element(LinkViewUrl) != null)
            {
                itemUrl = item.Element(LinkViewUrl).Value;
            }
            if (item.Element(LinkViewImage) != null)
            {
                itemBmpBytes = item.Element(LinkViewImage).Value;
                byte[] byteBuffer = Convert.FromBase64String(itemBmpBytes);
                using (MemoryStream ms = new MemoryStream(byteBuffer))
                {
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.EndInit();
                }
            }
            return new LinkItem(itemTitle, itemUrl, image, LinkItem.AppType.AppX);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.Globalization;

namespace JakStartScreen
{
    public static class XmlDb
    {
        public static readonly string LinkItemElement = "LinkItem";
        public static readonly string LinkItemType = "LinkType";
        public static readonly string LinkViewElement = "LinkView";
        public static readonly string LinkViewTitle = "Title";
        public static readonly string LinkViewUrl = "URL";
        public static readonly string LinkViewImage = "Image";
        public static readonly string LinkViewSize = "Size";
        public static readonly string LinkViewRow = "Row";
        public static readonly string LinkViewCol = "Column";
        public static readonly string LinkViewImageHeight = "Height";
        public static readonly string LinkViewImageWidth = "Width";
        public static readonly string LinkViewImageBytes = "Bytes";
        public static readonly string AppListRoot = "AppList";
        public static readonly string UserGroupsRoot = "UserGroups";
        public static readonly string UserGroupElement = "Group";
        public static readonly string UserGroupName = "GroupName";
        public static readonly string UserGroupRows = "GroupRows";
        public static readonly string UserGroupCount = "GroupCount";

        #region PublicLoadsave

        public static List<LinkItem> LoadAppListDb(string fileName)
        {
            List<LinkItem> list = new();
            XDocument xDoc = GetXDocumentFromFileName(fileName);
            if (xDoc.Root != null)
            {
                XElement xRoot = xDoc.Root;
                if (xRoot.Name == AppListRoot)
                {
                    foreach (XElement xElem in xRoot.Elements())
                    {
                        list.Add(XElementToLinkItem(xElem));
                    }
                }
            }
            return list;
        }

        public static bool SaveAppListDb(string fileName, List<LinkItem> items)
        {
            XDocument resDoc = new();
            XElement xRoot = new(AppListRoot);
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

        public static List<GroupView> LoadUserGroups(string fileName)
        {
            List<GroupView> list = new();
            XDocument xDoc = GetXDocumentFromFileName(fileName);
            if (xDoc.Root != null)
            {
                XElement xRoot = xDoc.Root;
                if (xRoot.Name == UserGroupsRoot)
                {
                    foreach (XElement xElem in xRoot.Elements())
                    {
                        list.Add(XElementToGroupView(xElem));
                    }
                }
            }
            return list;
        }

        public static bool SaveUserGroups(string fileName, List<GroupView> groups)
        {
            XDocument resDoc = new();
            XElement xRoot = new(UserGroupsRoot);
            foreach (GroupView item in groups)
            {
                xRoot.Add(GroupViewToXElement(item));
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

        #endregion PublicLoadsave

        #region Private XML Load Save

        private static XDocument GetXDocumentFromFileName(string FileName)
        {
            XDocument xDoc = new();
            if (File.Exists(FileName))
            {
                using (StreamReader sr = new(new FileStream(FileName, FileMode.Open)))
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

        #endregion Private XML Load Save

        #region Private Converters

        private static XElement IconSizeToXElement(IconSize size)
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

        private static XElement AppTypeToXElement(LinkItem.AppType appType)
        {
            string res;
            switch (appType)
            {
                case LinkItem.AppType.AppX: res = "AppX"; break;
                case LinkItem.AppType.Desktop: res = "Desktop"; break;
                case LinkItem.AppType.Website: res = "Web"; break;
                default: res = "Undefined"; break;
            }
            return new XElement(LinkItemType, res);
        }

        private static IconSize XElementToIconSize(string item)
        {
            IconSize iconSize;
            switch (item)
            {
                case "Tile1x1": iconSize = IconSize.Tile1x1; break;
                case "Tile1x4": iconSize = IconSize.Tile1x4; break;
                case "Tile2x2": iconSize = IconSize.Tile2x2; break;
                case "Tile2x4": iconSize = IconSize.Tile2x4; break;
                default: iconSize = IconSize.Tile1x1; break;
            }
            return iconSize;
        }

        private static byte[] XElementToImageBytes(string item)
        {
            return Convert.FromBase64String(item);
        }

        private static LinkItem.AppType XElementToAppType(string item)
        {
            LinkItem.AppType appType;
            switch (item)
            {
                case "AppX": appType = LinkItem.AppType.AppX; break;
                case "Desktop": appType = LinkItem.AppType.Desktop; break;
                case "Web": appType = LinkItem.AppType.Website; break;
                default: appType = LinkItem.AppType.Desktop; break;
            }
            return appType;
        }

        #endregion Private Converters

        #region Link to XML

        private static XElement LinkItemToXElement(LinkItem item)
        {
            XElement lnk = new(LinkViewElement);
            lnk.Add(new XElement(LinkViewTitle, item.Name));
            lnk.Add(new XElement(LinkViewUrl, item.LinkURL));
            lnk.Add(AppTypeToXElement(item.Type));
            lnk.Add(ImageToXElement(item.GetImageBytes()));
            return lnk;
        }

        private static XElement LinkViewToXElement(LinkItemView itemView)
        {
            XElement lnk = new(LinkViewElement);
            lnk.Add(LinkItemToXElement(itemView.ToLinkItem()));
            lnk.Add(IconSizeToXElement(itemView.Size));
            lnk.Add(new XElement(LinkViewRow, itemView.Row));
            lnk.Add(new XElement(LinkViewCol, itemView.Column));
            return lnk;
        }

        private static XElement GroupViewToXElement(GroupView group)
        {
            XElement grp = new(UserGroupElement);
            grp.Add(new XElement(UserGroupName, group.GroupName));
            grp.Add(new XElement(UserGroupRows, group.Rows));
            grp.Add(new XElement(UserGroupCount, group.Count));
            foreach (LinkItemView tile in group.GetTiles)
            {
                grp.Add(LinkViewToXElement(tile));
            }
            return grp;
        }

        #endregion Link to XML

        #region XML to Link

        private static LinkItem XElementToLinkItem(XElement item)
        {
            string itemTitle = "", itemUrl = "";
            LinkItem.AppType itemType = LinkItem.AppType.AppX;
            BitmapImage image = new();
            if (item.Element(LinkViewTitle) != null)
            {
                itemTitle = item.Element(LinkViewTitle).Value;
            }
            if (item.Element(LinkViewUrl) != null)
            {
                itemUrl = item.Element(LinkViewUrl).Value;
            }
            if (item.Element(LinkItemType) != null)
            {
                itemType = XElementToAppType(item.Element(LinkItemType).Value);
            }
            if (item.Element(LinkViewImage) != null)
            {
                byte[] byteBuffer = XElementToImageBytes(item.Element(LinkViewImage).Value);
                using (MemoryStream ms = new MemoryStream(byteBuffer))
                {
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.EndInit();
                }
            }
            return new LinkItem(itemTitle, itemUrl, image, itemType);
        }

        private static LinkItemView XElementToLinkView(XElement item)
        {
            LinkItem lnk = new();
            int iconRow = 0, iconCol = 0, parsedVal;
            IconSize iconSize = new();
            if (item.Element(LinkViewElement) != null)
            {
                lnk = XElementToLinkItem(item.Element(LinkViewElement));
            }
            if (item.Element(LinkViewSize) != null)
            {
                iconSize = XElementToIconSize(item.Element(LinkViewSize).Value);
            }
            if (item.Element(LinkViewCol) != null)
            {
                if (int.TryParse(item.Element(LinkViewCol).Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedVal))
                {
                    iconCol = parsedVal;
                }
            }
            if (item.Element(LinkViewRow) != null)
            {
                if (int.TryParse(item.Element(LinkViewRow).Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedVal))
                {
                    iconRow = parsedVal;
                }
            }
            // LinkItemView(LinkItem item, IconSize iconSize, int iconRow, int iconCol)
            return new LinkItemView(lnk, iconSize, iconRow, iconCol);
        }

        private static GroupView XElementToGroupView(XElement item)
        {
            GroupView grp = new();

            if (item.Element(UserGroupName) != null)
            {
                grp.GroupName = item.Element(UserGroupName).Value;
            }
            foreach (XElement element in item.Elements())
            {
                if (element.Name == LinkViewElement)
                {
                    grp.AddTile(XElementToLinkView(element));
                }
            }
            return grp;
        }

        #endregion XML to Link
    }
}
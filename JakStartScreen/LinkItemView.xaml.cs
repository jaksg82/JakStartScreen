using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace JakStartScreen
{
    public enum IconSize
    {
        Tile1x1,
        Tile1x4,
        Tile2x2,
        Tile2x4
    }

    /// <summary>
    /// Logica di interazione per LinkItemView.xaml
    /// </summary>
    public partial class LinkItemView : UserControl
    {
        private LinkItem _item;

        public string Title
        { get { return _item.Name; } }

        public BitmapImage Image
        { get { return _item.Image; } }

        public string Link
        { get { return _item.LinkURL; } }

        public byte[] ImageBytes
        { get { return _item.GetImageBytes(); } }

        public IconSize Size { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public LinkItemView()
        {
            InitializeComponent();
            _item = new LinkItem();
            ItemImage.Source = Image;
            ItemName.Text = Title;
            ChangeSize(IconSize.Tile1x1);
            Row = 0;
            Column = 0;
        }

        public LinkItemView(LinkItem item)
        {
            InitializeComponent();
            _item = item;
            ItemImage.Source = Image;
            ItemName.Text = Title;
            ChangeSize(IconSize.Tile1x1);
            Row = 0;
            Column = 0;
        }

        public LinkItemView(LinkItem item, IconSize iconSize, int iconRow, int iconCol)
        {
            InitializeComponent();
            _item = item;
            ItemImage.Source = Image;
            ItemName.Text = Title;
            ChangeSize(iconSize);
            Row = iconRow;
            Column = iconCol;
        }

        public void ChangeSize(IconSize iconSize)
        {
            Size = iconSize;
            switch (iconSize)
            {
                case IconSize.Tile1x1:
                    // Set Image Size
                    //Grid.SetColumnSpan(ItemImage, 2);
                    //Grid.SetRowSpan(ItemImage, 2);
                    ItemImage.Height = MainWindow.ImageTileSize1;
                    ItemImage.Width = MainWindow.ImageTileSize1;
                    // Set Title Size and Position
                    //Grid.SetColumn(ItemName, 1);
                    //Grid.SetRow(ItemName, 0);
                    //Grid.SetColumnSpan(ItemName, 1);
                    //Grid.SetRowSpan(ItemName, 2);
                    ItemName.Visibility = Visibility.Collapsed;
                    break;

                case IconSize.Tile1x4:
                    // Set Image Size
                    //Grid.SetColumnSpan(ItemImage, 1);
                    //Grid.SetRowSpan(ItemImage, 2);
                    ItemImage.Height = MainWindow.ImageTileSize1;
                    ItemImage.Width = MainWindow.ImageTileSize1;
                    // Set Title Size and Position
                    //Grid.SetColumn(ItemName, 1);
                    //Grid.SetRow(ItemName, 0);
                    //Grid.SetColumnSpan(ItemName, 1);
                    //Grid.SetRowSpan(ItemName, 2);
                    ItemName.Visibility = Visibility.Visible;
                    break;

                case IconSize.Tile2x2:
                    // Set Image Size
                    //Grid.SetColumnSpan(ItemImage, 1);
                    //Grid.SetRowSpan(ItemImage, 2);
                    ItemImage.Height = MainWindow.ImageTileSize2;
                    ItemImage.Width = MainWindow.ImageTileSize2;
                    // Set Title Size and Position
                    //Grid.SetColumn(ItemName, 1);
                    //Grid.SetRow(ItemName, 0);
                    //Grid.SetColumnSpan(ItemName, 1);
                    //Grid.SetRowSpan(ItemName, 2);
                    ItemName.Visibility = Visibility.Collapsed;
                    break;

                case IconSize.Tile2x4:
                    // Set Image Size
                    //Grid.SetColumnSpan(ItemImage, 1);
                    //Grid.SetRowSpan(ItemImage, 2);
                    ItemImage.Height = MainWindow.ImageTileSize2;
                    ItemImage.Width = MainWindow.ImageTileSize2;
                    // Set Title Size and Position
                    //Grid.SetColumn(ItemName, 1);
                    //Grid.SetRow(ItemName, 0);
                    //Grid.SetColumnSpan(ItemName, 1);
                    //Grid.SetRowSpan(ItemName, 2);
                    ItemName.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }

        public LinkItem ToLinkItem()
        {
            return _item;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JakStartScreen
{
    /// <summary>
    /// Logica di interazione per LinkItemView.xaml
    /// </summary>
    public partial class LinkItemView : UserControl
    {
        private LinkItem _item;
        public string Title { get; set; }
        public BitmapImage Image { get; set; }
        public string Link { get; set; }


        public LinkItemView()
        {
            InitializeComponent();
            _item = new LinkItem();
            Title = JakStartScreen.Language.Strings.NewShortcut;
            Image = new BitmapImage();
            Link = "";
            ItemImage.Source = Image;
            ItemName.Text = Title;
            ChangeSize(_item.Size);
        }

        public LinkItemView(LinkItem item)
        {
            _item = item;
            Title = _item.Name;
            Image = _item.Image;
            Link = _item.LinkURL;
            ItemImage.Source = Image;
            ItemName.Text = Title;
            ChangeSize(_item.Size);
        }

        public void ChangeSize(IconSize iconSize)
        {
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
                case IconSize.Tile1x2:
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
                case IconSize.Tile1x3:
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
                case IconSize.Tile2x3:
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


            }
        }
    }
}

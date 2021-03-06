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
    /// Logica di interazione per GroupView.xaml
    /// </summary>
    public partial class GroupView : UserControl
    {
        private List<LinkItemView> _tiles = new List<LinkItemView>();
        private int _rows;
        private int _count;

        public string GroupName { get; set; }

        public List<LinkItemView> GetTiles
        { get { return _tiles; } }

        public int Rows => _rows;

        public int Count => _count;

        public GroupView()
        {
            InitializeComponent();
            GroupName = JakStartScreen.Language.Strings.NewGroup;
            lblTitle.Content = GroupName;
        }

        public GroupView(string groupName, List<LinkItemView> linkItems)
        {
            _tiles = linkItems;
            GroupName = groupName;
        }

        public int CountRows()
        {
            int resRow = 1;
            if (_tiles.Count != 0)
            {
                foreach (LinkItemView item in _tiles)
                {
                    if (item.Size == IconSize.Tile1x1 || item.Size == IconSize.Tile1x4)
                    {
                        resRow = Math.Max(resRow, item.Row + 1);
                    }
                    else
                    {
                        resRow = Math.Max(resRow, item.Row + 2);
                    }
                }
            }
            return resRow;
        }

        public void AddTile(LinkItemView linkItem, bool isNew = true)
        {
            if (isNew)
            {
                linkItem.Row = CountRows() + 1;
                linkItem.Column = 0;
                _tiles.Add(linkItem);
            }
            else
            {
                _tiles.Add(linkItem);
            }
        }

        private int GetItemWidth(IconSize size)
        {
            switch (size)
            {
                case IconSize.Tile1x1: return 1 * MainWindow.BaseTileSize;
                case IconSize.Tile1x4: return 4 * MainWindow.BaseTileSize;
                case IconSize.Tile2x2: return 2 * MainWindow.BaseTileSize;
                case IconSize.Tile2x4: return 4 * MainWindow.BaseTileSize;
                default: return 0;
            }
        }

        private int GetItemHeight(IconSize size)
        {
            switch (size)
            {
                case IconSize.Tile1x1:
                case IconSize.Tile1x4: return 1 * MainWindow.BaseTileSize;
                case IconSize.Tile2x2:
                case IconSize.Tile2x4: return 2 * MainWindow.BaseTileSize;
                default: return 0;
            }
        }
    }
}
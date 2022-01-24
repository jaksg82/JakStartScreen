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

        public string GroupName { get; set; }

        public GroupView()
        {
            InitializeComponent();
            GroupName = JakStartScreen.Language.Strings.NewGroup;
            lblTitle.Content = GroupName;
        }

        public int CountRows()
        {
            int resRow = 1;
            if (_tiles.Count != 0)
            {
                foreach (LinkItemView item in _tiles)
                {
                    if (item.Size == IconSize.Tile1x1 || item.Size == IconSize.Tile1x2 || item.Size == IconSize.Tile1x3)
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
    }
}
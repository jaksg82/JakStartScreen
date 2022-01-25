using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JakStartScreen
{
    internal class LinkGroup
    {
        private string _title = "";
        private int _rows;
        private int _count;
        private List<LinkItem> _links;

        public string Title
        { get => _title; set { _title = value; } }

        public int Rows => _rows;

        public int Count => _count;

        public LinkGroup()
        {
            Title = "New group";
            _rows = 0;
            _count = 0;
            _links = new List<LinkItem>();
        }

        public bool AddLink(LinkItem item)
        {
            if (item != null)
            {
                _links.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        //private bool CheckPosition(LinkItem item)
        //{
        //    foreach(LinkItem link in _links)
        //    {
        //    }
        //    if(item.Size == IconSize.Tile1x1)
        //    {
        //    }
        //}

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